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
using AutoMapper;
using JornalerosApp.Shared.Entities;

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IDbServices<Persona> _dbServices;
        private readonly IMapper mapper = null;

        public PersonasController(IDbServices<Persona> dbServices, IMapper mapper)
        {
            _dbServices = dbServices ?? throw new ArgumentNullException(nameof(dbServices));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        [ProducesResponseType(typeof(PersonaModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PersonaModel), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<PersonaModel>> GetPersonas(string id)
        {
            var persona = await _dbServices.GetItemById(id);

            var personaModel = this.mapper.Map<Persona, PersonaModel>(persona);
            personaModel.Curriculum = this.mapper.Map<ICollection<Curriculum>, Curriculum[]>(persona.Curriculum);
            personaModel.RelacionOfertaPersona = this.mapper.Map<ICollection<RelacionOfertaPersona>, RelacionOfertaPersona[]>(persona.RelacionOfertaPersona);
            personaModel.Permiso = this.mapper.Map<ICollection<Permiso>, Permiso[]>(persona.Permiso);
            personaModel.Nacionalidad = this.mapper.Map<ICollection<Nacionalidad>, Nacionalidad[]>(persona.Nacionalidad);

            if (persona == null)
            {
                return Ok(new PersonaModel());
            }
            return Ok(personaModel);
        }

        //// GET: api/Personas/Nombre
        //[HttpGet("{Nombre}")]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(Persona), (int)HttpStatusCode.OK)]

        //public async Task<ActionResult<Persona>> GetPersonaByName(string nombre)
        //{
        //    var persona = await _dbServices.GetItemByname(nombre);

        //    if (persona == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(persona);

        //    //return Ok(this.mapper.Map<PersonaModel>(persona));
        //}

        // PUT: api/Personas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutPersonas(string id, [FromBody] PersonaModel personaModel)
        {
            try
            {
                if (id != personaModel.IdPersona)
                {
                    return BadRequest();
                }

                var nueva = this.mapper.Map<PersonaModel, Persona>(personaModel);
                await _dbServices.UpdateItem(nueva);

                //await _dbServices.UpdateItem(persona);           
             
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
        [ProducesResponseType(typeof(PersonaModel), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<PersonaModel>> PostPersonas([FromBody] PersonaModel personaModel)
        {            
            try
            {
                var persona = this.mapper.Map<PersonaModel, Persona>(personaModel);
                await _dbServices.AddItem(persona);

            }
            catch (DbUpdateException)
            {
                if (PersonaExists(personaModel.IdPersona))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonas", new { id = personaModel.IdPersona }, personaModel);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PersonaModel), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Persona>> DeletePersona(string id)
        {
            var persona = await _dbServices.GetItemById(id);
            if (persona == null)
            {
                return NotFound();
            }
            await _dbServices.DeleteItem(id);
            //return this.mapper.Map<PersonaModel>(persona);
            return Ok(persona);
        }

        private bool PersonaExists(string id)
        {
            return _dbServices.GetItemById(id).Result != null;
        }
    }
}
