using JornalerosApp.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfertasApp.Commands;
using OfertasApp.Data;
using OfertasApp.Queries;
using OfertasApp.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {
        private readonly IDbServices<Oferta> _context;
        private readonly IMediator _mediatr;

        public OfertasController(IDbServices<Oferta> context, IMediator mediatr)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }

        // GET: api/Empresas
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Oferta>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IReadOnlyList<Oferta>>> GetOferta()
        {
            var lista = await _context.GetAllAsync();
            return Ok(lista);
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Oferta), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Oferta), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Oferta>> GetOferta(string id)
        {
            try
            {
                var empresa = await _context.GetByIdAsync(id);

                if (empresa == null)
                {
                    return Ok(new Oferta());
                }
                return Ok(empresa);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                throw;
            }
        }

        // PUT: api/Empresas/5

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Put(string id, [FromBody] Oferta oferta)
        {
            try
            {
                if (id != oferta.IdOferta)
                {
                    return BadRequest();
                }

                await _context.UpdateAsync(oferta);

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

        //// POST: api/Empresas
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(Oferta), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Oferta>> Post([FromBody] Oferta oferta)
        {
            try
            {
                await _context.AddAsync(oferta);
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

            return CreatedAtAction("GetOferta", new { id = oferta.IdOferta }, oferta);
        }

        // DELETE: api/Empresas/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]

        public async Task<ActionResult> DeleteEmpresas(string id)
        {
            var result = await _context.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            await _context.DeleteAsync(result);
            return NoContent();
        }

        private bool OfertaExists(string id)
        {
            var result = _context.GetByIdAsync(id);
            return (result != null);
        }

        // GET: api/Personas        
        [HttpGet("GetOfertaByTitulo/{titulo}")]
        [ProducesResponseType(typeof(IReadOnlyList<OfertaResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<IReadOnlyList<OfertaResponse>>> GetOfertaByTitulo(string titulo)
        {
            try
            {
                var query = new OfertaQueries(titulo);
                var ofertas = await _mediatr.Send(query);
                if(ofertas.ToList().Count() > 0)
                {
                    return Ok(ofertas);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }

        // POST: api/Personas
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(OfertaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<ActionResult<OfertaResponse>> CheckOutOferta([FromBody] CheckOutOfertaCommand checkOutOfertaCommand)
        {
            if (ModelState.IsValid)
            {
                var oferta = await _mediatr.Send(checkOutOfertaCommand);
                return Ok(oferta);
            }
            return BadRequest();           
        }
    }
}
