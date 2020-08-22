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
        private readonly IMapper mapper;

        public EmpresasController(IDbServices<Empresa> context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Empresas
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Empresa>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            var lista = new List<EmpresaModel>();
            await _context.AllItems().ContinueWith((result) => 
                                                    result.Result.ToList().ForEach(p => {
                                                    lista.Add(this.mapper.Map<EmpresaModel>(p));            
            }));
            return Ok(lista);
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmpresaModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(EmpresaModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<EmpresaModel>> GetEmpresas(string id)
        {
            try
            {
                var empresa = await _context.GetItemById(id);
                var data = mapper.Map<Empresa, EmpresaModel>(empresa);
                data.Oferta = mapper.Map<ICollection<Oferta>, OfertaModel[]>(empresa.Oferta);
                if (empresa == null)
                {
                    return Ok(new EmpresaModel());
                }
                //int i = 0;
                
                //OfertaModel[] listaOfertaModel = new OfertaModel[empresa.Oferta.Count];
                //empresa.Oferta.ToList().ForEach(o =>
                //{
                //    var nuevo = new OfertaModel
                //    {
                //        Alojamiento = o.Alojamiento,
                //        CantidadPersonas = o.CantidadPersonas,
                //        ContinuidadIgualLabor = o.ContinuidadIgualLabor,
                //        ContinuidadOtraLabor = o.ContinuidadOtraLabor,
                //        Descripcion = o.Descripcion,
                //        FechaCaducidad = o.FechaCaducidad,
                //        FechaPublicacion = o.FechaPublicacion,
                //        IdEmpresa = o.IdEmpresa,
                //        IdOferta = o.IdOferta,
                //        JornadaReal = o.JornadaReal
                //    };
                //    listaOfertaModel[i] = nuevo;
                //    i++;
                //});

                //EmpresaModel result = new EmpresaModel
                //{
                //    Actividad = empresa.Actividad,
                //    Cargo = empresa.Cargo,
                //    CodigoPostal = empresa.CodigoPostal,
                //    CorreoElectronico = empresa.CorreoElectronico,
                //    Dirección = empresa.Dirección,
                //    IdEmpresa = empresa.IdEmpresa,
                //    Nifcif = empresa.Nifcif,
                //    NombreContacto = empresa.NombreContacto,
                //    NombreEmpresa = empresa.NombreEmpresa,
                //    Oferta = listaOfertaModel,
                //    Provincia = empresa.Provincia,
                //    Telefono = empresa.Telefono
                //};

                return Ok(data);
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
        public async Task<IActionResult> PutEmpresas(string id, [FromBody] EmpresaModel empresa)
        {
            if (id != empresa.IdEmpresa)
            {
                return BadRequest();
            }          

            try
            {
                var data = this.mapper.Map<EmpresaModel, Empresa>(empresa);
                data.Oferta = (ICollection<Oferta>)empresa.Oferta.ToList();
                await _context.UpdateItem(data);
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
        [ProducesResponseType(typeof(EmpresaModel), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<EmpresaModel>> PostEmpresas([FromBody] EmpresaModel empresa)
        {           
            try
            {
                await _context.AddItem(this.mapper.Map<Empresa>(empresa));
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<string>> DeleteEmpresas(string id)
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
