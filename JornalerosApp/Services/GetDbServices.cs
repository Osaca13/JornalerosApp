using JornalerosApp.Infrastructure.Data;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public class GetDbServices<T> : IGetDbServices<T> where T : class
    { 
        protected readonly ApplicationDbContext _dbContext;
        public GetDbServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
