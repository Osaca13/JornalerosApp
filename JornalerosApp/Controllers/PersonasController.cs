using AutoMapper;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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
        [ProducesResponseType(typeof(Persona), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Persona), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Persona>> GetPersonas(string id)
        {
            var persona = await _dbServices.GetItemById(id);

            //var personaModel = this.mapper.Map<Persona, PersonaModel>(persona);
            //personaModel.Curriculum = this.mapper.Map<ICollection<Curriculum>, Curriculum[]>(persona.Curriculum);
            //personaModel.RelacionOfertaPersona = this.mapper.Map<ICollection<RelacionOfertaPersona>, RelacionOfertaPersona[]>(persona.RelacionOfertaPersona);
            //personaModel.Permiso = this.mapper.Map<ICollection<Permiso>, Permiso[]>(persona.Permiso);
            //personaModel.Nacionalidad = this.mapper.Map<ICollection<Nacionalidad>, Nacionalidad[]>(persona.Nacionalidad);

            if (persona == null)
            {
                return Ok(new Persona());
            }
            return Ok(persona);
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutPersonas(string id, [FromBody] Persona persona)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (id != persona.IdPersona)
                    {
                        return BadRequest(ModelState);
                    }

                    //var nueva = this.mapper.Map<PersonaModel, Persona>(persona);
                   // var result = await TryUpdateModelAsync<Persona>(persona);
                    //await _dbServices.UpdateItem(persona);

                    var result = await _dbServices.UpdateItem(persona);           
                    if (result)
                    {
                        return Ok();
                    }
                }

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

        private void Validate<T>(T persona)
        {
            throw new NotImplementedException();
        }

        // POST: api/Personas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(Persona), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Persona>> PostPersonas([FromBody] Persona persona)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    await _dbServices.AddItem(persona);
                }
                //var persona = this.mapper.Map<PersonaModel, Persona>(persona);
               

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

            return CreatedAtAction("GetPersonas", new { id = persona.IdPersona }, persona);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Persona), (int)HttpStatusCode.OK)]

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

        //private BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
        //{
        //    //BadRequestObjectResult is class found Microsoft.AspNetCore.Mvc and is inherited from ObjectResult.    
        //    //Rest code is linq.    
        //    return new BadRequestObjectResult(actionContext.ModelState
        //     .Where(modelError => modelError.Value.Errors.Count > 0)
        //     .Select(modelError => new Error
        //     {
        //         ErrorField = modelError.Key,
        //         ErrorDescription = modelError.Value.Errors.FirstOrDefault().ErrorMessage
        //     }).ToList());
        //}




        //GetType().GetRuntimeProperty(this.Nombre);

        //foreach(var member in attributes.Values)
        //{
        //    if(member.Name == "Dni")
        //    {
        //        var data = validacionNIF.GetValidationResult(Dni, validationContext);
        //        if (!string.IsNullOrEmpty(data?.ErrorMessage))
        //        {
        //            results.Add(new ValidationResult("Identificacion no válida"));
        //        }
        //    }
        //    else
        //    {
        //        
        //    }               
        //}

       


    }    
}
