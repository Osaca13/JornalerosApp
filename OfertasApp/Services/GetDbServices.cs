using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using OfertasApp.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfertasApp.Services
{
    public class GetDbServices<T> : IGetDbServices<T> where T : class
    { 
        protected readonly OfertaDataContext _dbContext;
        public GetDbServices(OfertaDataContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
