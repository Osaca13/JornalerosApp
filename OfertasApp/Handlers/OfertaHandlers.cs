using AutoMapper;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using MediatR;
using OfertasApp.Queries;
using OfertasApp.Responses;
using OfertasApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OfertasApp.Handlers
{
    public class OfertaHandlers : IRequestHandler<OfertaQueries, IEnumerable<OfertaResponse>>
    {
        private readonly IOfertaDbServices _dbServices;
        private readonly IMapper _mapper;

        public OfertaHandlers(IOfertaDbServices dbServices, IMapper mapper)
        {
            _dbServices = dbServices ?? throw new ArgumentNullException(nameof(dbServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<OfertaResponse>> Handle(OfertaQueries request, CancellationToken cancellationToken)
        {
            string titulo = request.GetOfertaByTitulo.ToString();
            Expression<Func<Oferta, bool>> predicate = x => x.Titulo == titulo;

            IReadOnlyList<Data.Oferta> listOfOfertasWithTitulo = await _dbServices.GetByTituloAsync(titulo);
            return _mapper.Map<IReadOnlyList<OfertaResponse>>(listOfOfertasWithTitulo);

        }
    }
}
