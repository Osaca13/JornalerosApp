using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JornalerosApp.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresa()
        {
            return Ok(await _context.AllItems());
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(string id)
        {
            var empresa = await _context.GetItemById(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        // PUT: api/Empresas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(string id, Empresa empresa)
        {
            if (id != empresa.IdEmpresa)
            {
                return BadRequest();
            }          

            try
            {
                await _context.UpdateItem(empresa);
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
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {           
            try
            {
                await _context.AddItem(empresa);
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

            return CreatedAtAction("GetEmpresa", new { id = empresa.IdEmpresa }, empresa);
        }

        // DELETE: api/Empresas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteEmpresa(string id)
        {

            var deleteResult = await _context.DeleteItem(id);
            if (!deleteResult)
            {
                return NotFound();
            }
            return Ok(id);
        }

        private bool EmpresaExists(string id)
        {
            var result = _context.GetItemById(id);
            return (result != null);
        }
    }
}
