using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private IDbServices<Persona> _personaDbServices;
        //private ILogger _logger;
        public PersonaController(IDbServices<Persona> personaDbServices)
        {
            _personaDbServices = personaDbServices;
            //_logger = logger;
        }
        // GET: api/<PersonaController>
        [HttpGet]
        public Task<List<Persona>> Get()
        {
            return _personaDbServices.AllItem();
        }

        // GET api/<PersonaController>/5
        [HttpGet("{id}")]
        public Task<Persona> Get(string id)
        {
           return _personaDbServices.GetItemById(id);            
        }

        // POST api/<PersonaController>
        [HttpPost]
        public Task<EntityEntry<Persona>> Post([FromBody] Persona persona)
        {
           return _personaDbServices.AddItem(persona);            
        }

        // PUT api/<PersonaController>/5
        [HttpPut("{id}")]
        
        public void Put(string id, [FromBody] Persona persona)
        {           
           _personaDbServices.UpdateItem(id, persona);
           Debug.WriteLine("persona actualizada");
        }

        // DELETE api/<PersonaController>/5
        [HttpDelete("{id}")]
       
        public void Delete(string id)
        {
            _personaDbServices.DeleteItem(id);
          
        }
    }
}
