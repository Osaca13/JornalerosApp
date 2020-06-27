using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JornalerosApp.Shared.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JornalerosApp.Services
{
    public class PersonaDbServices : IPersonaDbServices
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


            return _context.Persona.AddAsync(persona).AsTask();


        }

        public void DeletePersona(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Persona> GetPersonaById(int id)
        {

            return _context.Persona.FindAsync(id).AsTask();


        }

        public void UpdatePersona(int id, Persona persona)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
