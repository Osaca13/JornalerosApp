using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Net;
using AutoMapper;
using JornalerosApp.Shared.Entities;
using System.Diagnostics;

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IDbServices<Empresa> _context;

        public EmpresasController(IDbServices<Empresa> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Empresas
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Empresa>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IReadOnlyList<Empresa>>> GetEmpresas()
        {
            var lista = await _context.GetAllAsync(); 
            return Ok(lista);
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Empresa), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Empresa), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Empresa>> GetEmpresaById(string id)
        {
            try
            {
                var empresa = await _context.GetByIdAsync(id);
                
                if (empresa == null)
                {
                    return Ok(new Empresa());
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
        public async Task<IActionResult> PutEmpresas(string id, [FromBody] Empresa empresa)
        {
            try
            {
                if (id != empresa.IdEmpresa)
                {
                    return BadRequest();
                }

                await _context.UpdateAsync(empresa);
                
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

        // POST: api/Empresas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(Empresa), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<EmpresaModel>> PostEmpresas([FromBody] Empresa empresa)
        {           
            try
            {
                await _context.AddAsync(empresa);
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

            return CreatedAtAction("GetEmpresas", new { id = empresa.IdEmpresa }, empresa);
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

        private bool EmpresaExists(string id)
        {
            var result = _context.GetByIdAsync(id);
            return (result != null);
        }
    }
}
