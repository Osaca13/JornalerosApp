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
    public class CheckOutPersonaHandler : IRequestHandler<CheckOutPersonaCommand, PersonaResponse>
    {
        public readonly IDbServices<Persona> dbservices;

        public CheckOutPersonaHandler(IDbServices<Persona> dbservices)
        {
            this.dbservices = dbservices ?? throw new ArgumentNullException(nameof(dbservices));
        }

        public async Task<PersonaResponse> Handle(CheckOutPersonaCommand request, CancellationToken cancellationToken)
        {
            var personaEntity = PersonaMapper.Mapper.Map<Persona>(request);
            if (personaEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newOrder = await this.dbservices.AddAsync(personaEntity);

            return PersonaMapper.Mapper.Map<PersonaResponse>(newOrder);
        }
    }
}
