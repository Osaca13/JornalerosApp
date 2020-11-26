using JornalerosApp.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace JornalerosApp.Application.Queries
{
    public class OfertaQueries : IRequest<IEnumerable<OfertaResponse>>
    {
    }
}
