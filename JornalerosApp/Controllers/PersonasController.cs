using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JornalerosApp.Data;
using JornalerosApp.Shared.Models;
using System.Net;
using JornalerosApp.Shared.Services;

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IDbServices<Persona> _dbServices;

        public PersonasController(IDbServices<Persona> dbServices)
        {
            _dbServices = dbServices ?? throw new ArgumentNullException(nameof(dbServices));
        }

        // GET: api/Personas
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Persona>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            var result = await _dbServices.AllItems();
            return Ok(result);
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Persona), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Persona>> GetPersona(string id)
        {
            var persona = await _dbServices.GetItemById(id);

            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        // GET: api/Personas/Nombre
        [HttpGet("{Nombre}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Persona), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Persona>> GetPersonaByName(string nombre)
        {
            var persona = await _dbServices.GetItemByname(nombre);

            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        // PUT: api/Personas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutPersona(string id, [FromBody] Persona persona)
        {
            try
            {
                if (id != persona.IdPersona)
                {
                    return BadRequest();
                }

                await _dbServices.UpdateItem(persona);           
             
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Personas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(Persona), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Persona>> CreatePersona([FromBody] Persona persona)
        {            
            try
            {
               await _dbServices.AddItem(persona);
            }
            catch (DbUpdateException)
            {
                if (PersonaExists(persona.IdPersona))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersona", new { id = persona.IdPersona }, persona);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Persona>> DeletePersona(string id)
        {
            var persona = await _dbServices.GetItemById(id);
            if (persona == null)
            {
                return NotFound();
            }
            await _dbServices.DeleteItem(id);
            return persona;
        }

        private bool PersonaExists(string id)
        {
            return _dbServices.GetItemById(id).Result != null;
        }
    }
}
