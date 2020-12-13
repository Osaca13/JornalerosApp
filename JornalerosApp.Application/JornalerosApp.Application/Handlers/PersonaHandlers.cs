using JornalerosApp.Application.Queries;
using JornalerosApp.Application.Responses;
using JornalerosApp.Infrastructure.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace JornalerosApp.Application.Handlers
{
    public class PersonaHandlers : IRequestHandler<PersonaQueries, IEnumerable<Experiencia>>
    {
        private readonly IDbServices<Experiencia> dbServices;

        public PersonaHandlers(IDbServices<Experiencia> dbServices)
        {
            this.dbServices = dbServices ?? throw new ArgumentNullException(nameof(dbServices));
        }

        public async Task<IEnumerable<Experiencia>> Handle(PersonaQueries request, CancellationToken cancellationToken)
        {
            
            Expression<Func<Experiencia, bool>> lambda0 = num => request.GetPersonaByPuesto.Puesto == "Plantacion" || request.GetPersonaByPuesto.Puesto == "Cultivo";
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
