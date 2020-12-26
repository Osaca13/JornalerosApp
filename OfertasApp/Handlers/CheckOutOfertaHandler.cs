using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using MediatR;
using OfertasApp.Commands;
using OfertasApp.Mapper;
using OfertasApp.Responses;
using OfertasApp.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OfertasApp.Handlers
{
    public class CheckOutOfertaHandler : IRequestHandler<CheckOutOfertaCommand, OfertaResponse>
    {
        public readonly IOfertaDbServices dbServices;

        public CheckOutOfertaHandler(IOfertaDbServices dbServices)
        {
            this.dbServices = dbServices ?? throw new ArgumentNullException(nameof(dbServices));
        }

        public async Task<OfertaResponse> Handle(CheckOutOfertaCommand request, CancellationToken cancellationToken)
        {
            var ofertaEntity = OfertaMapper.Mapper.Map<Data.Oferta>(request);
            if (ofertaEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");
            
            ofertaEntity.IdOferta = Guid.NewGuid().ToString();
            var nuevaOferta = await dbServices.AddOfertaAsync(ofertaEntity);
            return OfertaMapper.Mapper.Map<OfertaResponse>(nuevaOferta);
        }
    }
}
