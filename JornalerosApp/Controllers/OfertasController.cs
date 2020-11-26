using AutoMapper;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {
        private readonly IDbServices<Oferta> _dbServices;
        

        public OfertasController(IDbServices<Oferta> dbServices)
        {
            _dbServices = dbServices ?? throw new ArgumentNullException(nameof(dbServices));
        }

        // GET: api/Personas
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Oferta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<Oferta>>> GetOfertas()
        {
            var result = await _dbServices.GetAllAsync();
            
            return Ok(result);
        }

        // GET: api/Personas
        [Route("Buscar")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Oferta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Oferta>>> Get([FromQuery] string actividad, [FromQuery] string lugar, [FromQuery] string sector)
        {
            //var result = await _dbServices.GetAsync(predicate: );
            //result.AsQueryable().Where(p => p.Titulo.Contains(actividad) && p.LugarTrabajo.Contains(lugar)).Select(p => p).ToList(); 
            return new List<Oferta>();
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Oferta), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Oferta), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Oferta>> GetOfertas(string id)
        {
            var oferta = await _dbServices.GetByIdAsync(id);
            
            if (oferta == null)
            {
                return Ok(new Oferta());
            }
            return Ok(oferta);
        }

        //// GET: api/Personas/Nombre
        //[HttpGet("{Nombre}")]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(Oferta), (int)HttpStatusCode.OK)]

        //public async Task<ActionResult<Oferta>> GetPersonaByName(string nombre)
        //{
        //    var Oferta = await _dbServices.GetItemByname(nombre);

        //    if (Oferta == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(Oferta);

        //    //return Ok(this.mapper.Map<PersonaModel>(Oferta));
        //}

        // PUT: api/Personas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutOfertas(string id, [FromBody] Oferta oferta)
        {
            try
            {
                if (id != oferta.IdOferta)
                {
                    return BadRequest();
                }

                //var nueva = this.mapper.Map<Oferta>(oferta);
                await _dbServices.UpdateAsync(oferta);

                //await _dbServices.UpdateItem(Oferta);           
             
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertaExists(id))
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
        [ProducesResponseType(typeof(Oferta), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Oferta>> PostOfertas([FromBody] Oferta oferta)
        {            
            try
            {
               await _dbServices.AddAsync(oferta);

            }
            catch (DbUpdateException)
            {
                if (OfertaExists(oferta.IdOferta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetOfertas", new { id = oferta.IdOferta }, oferta);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Oferta>> DeleteOferta(string id)
        {
            var oferta = await _dbServices.GetByIdAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }
            await _dbServices.DeleteAsync(oferta);
            //return this.mapper.Map<PersonaModel>(Oferta);
            return oferta;
        }

        private bool OfertaExists(string id)
        {
            return _dbServices.GetByIdAsync(id).Result != null;
        }
    }
}
