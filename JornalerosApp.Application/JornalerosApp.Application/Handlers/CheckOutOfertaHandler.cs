using JornalerosApp.Application.Commands;
using JornalerosApp.Application.Mapper;
using JornalerosApp.Application.Responses;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JornalerosApp.Application.Handlers
{
    public class CheckOutOfertaHandler : IRequestHandler<CheckOutOfertaCommand, OfertaResponse>
    {
        public readonly IDbServices<Oferta> dbservices;

        public CheckOutOfertaHandler(IDbServices<Oferta> dbservices)
        {
            this.dbservices = dbservices ?? throw new ArgumentNullException(nameof(dbservices));
        }

        public async Task<OfertaResponse> Handle(CheckOutOfertaCommand request, CancellationToken cancellationToken)
        {
            var ofertaEntity = OfertaMapper.Mapper.Map<Oferta>(request);
            if (ofertaEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newOrder = await this.dbservices.AddAsync(ofertaEntity);

            return OfertaMapper.Mapper.Map<OfertaResponse>(newOrder);
        }
    }
}
