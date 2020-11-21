using JornalerosApp.Infrastructure.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public class MunicipiosDbServices : IGetDbServices<RelacionMunicipioProvincia>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MunicipiosDbServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        

        public async Task<IEnumerable<RelacionMunicipioProvincia>> AllItems()
        {
            return await _applicationDbContext.RelacionMunicipioProvincia.ToListAsync();
        }

        

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        

       

        

        
    }
}
