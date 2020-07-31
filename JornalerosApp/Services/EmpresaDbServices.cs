using JornalerosApp.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<Empresa> GetItemById(string id)
        {
            return await _applicationDbContext.Empresa.Where(p => p.IdEmpresa == id).FirstOrDefaultAsync();
        }

        public Task<Empresa> GetItemByname(string name)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public Task<bool> UpdateItem(Empresa item)
        {
            throw new NotImplementedException();
        }
    }
}
