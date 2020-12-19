using JornalerosApp.Shared.Services;
using MediatR;
using OfertasApp.Queries;
using OfertasApp.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OfertasApp.Handlers
{
    public class OfertaHandlers : IRequestHandler<OfertaQueries, IEnumerable<OfertaResponse>>
    {
        private readonly IDbServices<OfertaResponse> dbServices;

        public OfertaHandlers(IDbServices<OfertaResponse> dbServices)
        {
            this.dbServices = dbServices ?? throw new ArgumentNullException(nameof(dbServices));
        }

        public async Task<IEnumerable<OfertaResponse>> Handle(OfertaQueries request, CancellationToken cancellationToken)
        {
            
            Expression<Func<OfertaResponse, bool>> lambda0 = num => request.GetOfertaByTitulo == "Plantacion" || request.GetOfertaByTitulo == "Cultivo";
            //ParameterExpression puestoParam1 = Expression.Parameter(typeof(string), "Puesto");
            //ParameterExpression puestoParam2 = Expression.Parameter(typeof(string), "Puesto");
            //ConstantExpression plantacion = Expression.Constant(request.GetPersonaByPuesto, typeof(Experiencia));
            //ConstantExpression cultivo = Expression.Constant("Cultivo", typeof(string));
            //BinaryExpression equalsA = Expression.Equal(puestoParam1, plantacion);
            //BinaryExpression equalsB = Expression.Equal(puestoParam2, cultivo);
            //Expression<Func<Experiencia, bool>> lambda1 =
            //Expression.Lambda<Func<Experiencia, bool>>(equalsA, new ParameterExpression[] { puestoParam1 });
            //Expression<Func<Experiencia, bool>> lambda2 =
            //Expression.Lambda<Func<Experiencia, bool>>(equalsB, new ParameterExpression[] { puestoParam2 });
          
            return await dbServices.GetAsync(lambda0);
        }
    }
}
