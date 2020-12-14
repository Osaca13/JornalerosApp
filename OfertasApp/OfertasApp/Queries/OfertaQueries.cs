using MediatR;
using OfertasApp.Responses;
using System;
using System.Collections.Generic;

namespace OfertasApp.Queries
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
