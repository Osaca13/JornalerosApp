using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private IPersonaDbServices _personaDbServices;
        public PersonaController(IPersonaDbServices personaDbServices)
        {
            _personaDbServices = personaDbServices;
        }
        // GET: api/<PersonaController>
        [HttpGet]
        public Task<List<Persona>> Get()
        {
            return _personaDbServices.AllPersonas();
        }

        // GET api/<PersonaController>/5
        [HttpGet("{id}")]
        public Task<Persona> Get(string id)
        {
            return _personaDbServices.GetPersonaById(id);
        }

        // POST api/<PersonaController>
        [HttpPost]
        public void Post([FromBody] Persona persona)
        {
            _personaDbServices.AddPersona(persona);
        }

        // PUT api/<PersonaController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Persona value)
        {
        }

        // DELETE api/<PersonaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
