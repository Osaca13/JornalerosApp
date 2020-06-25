using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JornalerosApp.Shared.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JornalerosApp.Services
{

    public class PersonaServices : IPersonaServices
    {
        private ApplicationDbContext _applicationDbContext;

        public PersonaServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Persona>> AllPersonas()
        {
            return await _applicationDbContext.Persona.ToListAsync();
        }


        public Persona GetPersonaById(int id)
        {
            return _applicationDbContext.Persona.Where(p => p.IdPersona == id).FirstOrDefault();
        }


        public void AddPersona(Persona persona)
        {
            if (_applicationDbContext.Persona.FindAsync(persona).IsFaulted)
            {
                _applicationDbContext.Persona.AddAsync(persona);
            }
        }


        public void UpdatePersona(int id, Persona persona)
        {
            if (_applicationDbContext.Persona.Any(p => p.IdPersona == id))
            {
                _applicationDbContext.Persona.Update(persona);
            }
        }


        public void DeletePersona(int id)
        {
            if (_applicationDbContext.Persona.Any(p => p.IdPersona == id))
            {

                _applicationDbContext.Persona.Remove(_applicationDbContext.Persona.Where(p => p.IdPersona == id).FirstOrDefault());
            }
        }
    }
}
