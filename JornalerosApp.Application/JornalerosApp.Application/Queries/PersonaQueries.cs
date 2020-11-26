using JornalerosApp.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JornalerosApp.Application.Queries
{
    public class PersonaQueries: IRequest<IEnumerable<PersonaResponse>>
    {
    }
}
