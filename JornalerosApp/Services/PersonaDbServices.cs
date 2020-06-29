using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JornalerosApp.Data;
using JornalerosApp.Shared.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JornalerosApp.Services
{
    public class PersonaDbServices : IPersonaDbServices, IDisposable
    {
        private ApplicationDbContext _context;

        public PersonaDbServices(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public Task<List<Persona>> AllPersonas()
        {
            return _context.Persona.ToListAsync();
       
        }

        public Task<EntityEntry<Persona>> AddPersona(Persona persona)
        {
            var c = _context.Persona.AddAsync(persona).AsTask();
            this.Save();
            return c;
        }

        public async Task<EntityEntry<Persona>> DeletePersona(string id)
        {
            Persona persona = await _context.Persona.FindAsync(id).AsTask();
            var entry = _context.Persona.Remove(persona);
            this.Save();
            return entry;
        }

        public Task<Persona> GetPersonaById(string id)
        {
            return _context.Persona.FindAsync(id).AsTask();            
        }

        public EntityEntry<Persona> UpdatePersona(string id, Persona persona)
        {
            var entry = _context.Persona.Update(persona);            
            this.Save();
            return entry;
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
    }
}
