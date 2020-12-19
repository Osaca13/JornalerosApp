using AutoMapper;
using EventBusRabbitMQ.Events;
using JornalerosApp.Shared.Models;
using OfertasApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofertas.Mapping
{
    public class OfertaMapping : Profile
    {
        public OfertaMapping()
        {
            CreateMap<CheckOutOfertaCommand, OfertaCheckoutEvent>().ReverseMap();
        }
    }
}
