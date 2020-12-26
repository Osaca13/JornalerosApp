using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using JornalerosApp.Shared.Services;
using MediatR;
using Newtonsoft.Json;
using OfertasApp.Commands;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace OfertasApp.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDbServices<Data.Oferta> _dbServices;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper, IDbServices<Data.Oferta> dbServices)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbServices = dbServices ?? throw new ArgumentNullException(nameof(dbServices));
        }

        public void Consume()
        {

            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.OfertaCheckoutQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(EventBusConstants.OfertaCheckoutQueue, true, consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.OfertaCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var ofertaCheckoutEvent = JsonConvert.DeserializeObject<OfertaCheckoutEvent>(message);

                //// EXECUTION : Call Internal Checkout Operation
                CheckOutOfertaCommand command = _mapper.Map<CheckOutOfertaCommand>(ofertaCheckoutEvent);
                await _mediator.Send(command);                
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
