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
    public class EmpresaDbServices : IDbServices<Empresa>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmpresaDbServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<bool> AddItem(Empresa item)
        {
            await _applicationDbContext.Empresa.AddAsync(item);
            await Save();
            return true;
            
        }

        public async Task<IEnumerable<Empresa>> AllItems()
        {
            return await _applicationDbContext.Empresa.ToListAsync();
        }

        public async Task<bool> DeleteItem(string id)
        {
            var result = await _applicationDbContext.Empresa.FindAsync(id);
            if (result == null)
            {
                return false;
            }

            var entry = _applicationDbContext.Empresa.Remove(result);
            await Save();
            return entry.State == EntityState.Deleted;
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

        public async Task<Empresa> GetItemById(string id)
        {
            return await _applicationDbContext.Empresa.Where(p => p.IdEmpresa == id).Include(p => p.Oferta).FirstOrDefaultAsync();
        }

        public async Task<Empresa> GetItemByname(string name)
        {
            return await _applicationDbContext.Empresa.Where(p => p.NombreEmpresa == name).FirstOrDefaultAsync();
        }

        public async Task Save()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateItem(Empresa item)
        {
            try
            {
                var result = _applicationDbContext.Entry<Empresa>(item);
                result.State = EntityState.Modified;
                await Save();
                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error updating Empresa: " + exc.Message);
                throw;
            }
        }
    }
}
