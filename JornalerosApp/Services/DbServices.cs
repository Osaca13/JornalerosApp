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
    public class DbServices<T>: IDbServices<T> where T : class 
    {
        private ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DbServices(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;            
        }

        public Task<List<Persona>> AllPersonas()
        {
            return _context.Persona
                .Include( p => p.Curriculum)
                .Include(p => p.Nacionalidad)
                .Include(p => p.Permiso)
                .Include(p => p.RelacionOfertaPersona)
                .ToListAsync();
       
        }

        public async Task<EntityEntry<Persona>> AddPersona(Persona persona)
        {
            var c = await _context.Persona.AddAsync(persona).AsTask();
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

        public async Task<Persona> GetPersonaById(string id)
        {
            return await _context.Persona.FindAsync(id).AsTask();            
        }

        public EntityEntry<T> UpdateItem(string id, T item)
        {
            try
            {
                var result = _context.Entry<T>(item);
                result.State = EntityState.Modified;
                Save();
                return result;
            }catch(Exception exc)
            {
                _logger.LogError("Error updating"+ exc.Message, item.ToString());
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

        public Task<EntityEntry<Persona>> AddItem(Persona item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Persona>> AllItem()
        {
            throw new NotImplementedException();
        }

        public Task<EntityEntry<Persona>> DeleteItem(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Persona> GetItemById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EntityEntry<T>> AddItem(T item)
        {
            throw new NotImplementedException();
        }
        
        public EntityEntry<T> UpdatePersona(string id, T item)
        {
            throw new NotImplementedException();
        }

        Task<List<T>> IDbServices<T>.AllItem()
        {
            throw new NotImplementedException();
        }

        Task<EntityEntry<T>> IDbServices<T>.DeleteItem(string id)
        {
            throw new NotImplementedException();
        }

        Task<T> IDbServices<T>.GetItemById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
