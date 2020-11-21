using JornalerosApp.Infrastructure.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public class PersonaDbServices: IDbServices<Persona>  
    {
        private readonly ApplicationDbContext _context;
        //private readonly ILogger _logger;

        public PersonaDbServices(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
                   
        }

        public async Task<IEnumerable<Persona>> AllItems()
        {
            return await _context
                .Persona
                //.Include(p => p.Curriculum)
                // .Include(p => p.Nacionalidad)
                // .Include(p => p.Permiso)
                // .Include(p => p.RelacionOfertaPersona)
                .ToListAsync();
        }

        public async Task<Persona> GetItemById(string id)
        {
            try
            {
                var data = await _context.Persona.Where(p => p.IdPersona == id)
                //.Include(p => p.Curriculum)
                // .Include(p => p.Nacionalidad)
                // .Include(p => p.Permiso)
                // .Include(p => p.RelacionOfertaPersona)
                 .FirstOrDefaultAsync();
                return data;
            }catch(Exception exc)
            {
                Debug.WriteLine(exc.Message);
                throw;
            }
        }

        public async Task<Persona> GetItemByname(string name)
        {
             return await _context.Persona.Where(p => p.Nombre == name).FirstOrDefaultAsync();
        }

        public async Task<bool> AddItem(Persona item)
        {
            try
            {
                var result = await _context.Persona.AddAsync(item);
                await Save();
                return result.State == EntityState.Added;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error añadiendo nuevo item: " + exc.Message);
                throw;

            }
        }

        public async Task<bool> UpdateItem(Persona item)
        {
            try
            {
                var result = _context.Entry<Persona>(item);
                result.State = EntityState.Modified;
                await Save();
                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error actualizando item existente: " + exc.Message);
                throw;
            }           
        }
        public async Task<bool> DeleteItem(string id)
        {
            var result = await _context.Persona.FindAsync(id);
            if(result == null)
            {
                return false;
            }

            var entry = _context.Persona.Remove(result);
            await Save();
            return entry.State == EntityState.Deleted;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
