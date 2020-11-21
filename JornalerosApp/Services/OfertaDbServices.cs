using JornalerosApp.Infrastructure.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public class OfertaDbServices : IDbServices<Oferta>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OfertaDbServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<bool> AddItem(Oferta item)
        {
            await _applicationDbContext.Oferta.AddAsync(item);
            await Save();
            return true;
            
        }

        public async Task<IEnumerable<Oferta>> AllItems()
        {
            return await _applicationDbContext.Oferta.ToListAsync();
        }

        public Task<bool> DeleteItem(string id)
        {
            throw new NotImplementedException();
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

        public async Task<Oferta> GetItemById(string id)
        {
            return await _applicationDbContext.Oferta.Where(p => p.IdEmpresa == id).FirstOrDefaultAsync();
        }

        public async Task<Oferta> GetItemByname(string name)
        {
            return await _applicationDbContext.Oferta.Where(p => p.Titulo == name).FirstOrDefaultAsync();
        }

        public async Task Save()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateItem(Oferta item)
        {
            try
            {
                var result = _applicationDbContext.Entry<Oferta>(item);
                result.State = EntityState.Modified;
                await Save();
                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error updating" + exc.Message);
                throw;
            }
        }
    }
}
