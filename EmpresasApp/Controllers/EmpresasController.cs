using EmpresasApp.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmpresasApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly IDbServices<Empresa> _context;

        public EmpresasController(IDbServices<Empresa> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Empresa>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<IEnumerable<Empresa>>> Get()
        {
            var data = await _context.GetAllAsync();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Empresa), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<Empresa>> Get(string id)
        {
            var data = await _context.GetAsync(x => x.IdEmpresa == id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data.First());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Empresa), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<Empresa>> Post([FromBody] Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.AddAsync(empresa);
                }
            }
            catch (DbUpdateException)
            {
                if (EmpresaExists(empresa.IdEmpresa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Get", new { id = empresa.IdEmpresa }, empresa);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Put(string id, [FromBody] Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != empresa.IdEmpresa)
                    {
                        return BadRequest(ModelState);
                    }

                    await _context.UpdateAsync(empresa);
                    return Ok();

                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
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

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Empresa), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Empresa>> Delete(string id)
        {
            var empresa = await _context.GetByIdAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            await _context.DeleteAsync(empresa);
            return Ok(empresa);
        }

        private bool EmpresaExists(string idEmpresa)
        {
            return _context.GetByIdAsync(idEmpresa).Result != null;
        }
    } 
}
