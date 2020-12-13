using JornalerosApp.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace JornalerosApp.Application.Queries
{
    public class OfertaQueries : IRequest<IEnumerable<OfertaResponse>>
    {
        public OfertaQueries(string getOfertaByTitulo)
        {
            GetOfertaByTitulo = getOfertaByTitulo ?? throw new ArgumentNullException(nameof(getOfertaByTitulo));
        }

        public string GetOfertaByTitulo { get; set; }
    }
}
