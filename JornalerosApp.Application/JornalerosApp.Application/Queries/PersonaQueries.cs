using JornalerosApp.Application.Responses;
using JornalerosApp.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JornalerosApp.Application.Queries
{
    public class PersonaQueries: IRequest<IEnumerable<Experiencia>>
    {
        public PersonaQueries(Experiencia getPersonaByPuesto)
        {
            GetPersonaByPuesto = getPersonaByPuesto ?? throw new ArgumentNullException(nameof(getPersonaByPuesto));
        }

        public Experiencia GetPersonaByPuesto { get; set; }
    }
}
