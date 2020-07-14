using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JornalerosApp.Data;
using JornalerosApp.Shared.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace JornalerosApp.Services
{
    public class PersonaDbServices: IDbServices<Persona>  
    {
        private readonly ApplicationDbContext _context;
        //private readonly ILogger _logger;

        public PersonaDbServices(ApplicationDbContext context)
        {
            _context = context;
            //_logger = logger;            
        } 

        public EntityEntry<Persona> UpdateItem(string id, Persona item)
        {
            try
            {
                var result = _context.Entry<Persona>(item);
                result.State = EntityState.Modified;
                Save();
                return result;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error updating" + exc.Message);
                throw;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
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

        public async Task<EntityEntry<Persona>> AddItem(Persona item)
        {
            var c = await _context.Persona.AddAsync(item).AsTask();
            this.Save();
            return c;
        }

        public async Task<List<Persona>> AllItem()
        {
            return await _context.Persona
                 .Include(p => p.Curriculum)
                 .Include(p => p.Nacionalidad)
                 .Include(p => p.Permiso)
                 .Include(p => p.RelacionOfertaPersona)
                 .ToListAsync();
        }

        public async Task<EntityEntry<Persona>> DeleteItem(string id)
        {
            Persona persona = await _context.Persona.FindAsync(id).AsTask();
            var entry = _context.Persona.Remove(persona);
            this.Save();
            return entry;
        }

        public async Task<Persona> GetItemById(string id)
        {
            return await _context.Persona.FindAsync(id).AsTask();
        }
    }
}
