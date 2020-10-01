using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JornalerosApp.Data;
using JornalerosApp.Services;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProvinciasController : Controller
    {
        public readonly IGetDbServices<RelacionMunicipioProvincia> _context;
        public ProvinciasController(IGetDbServices<RelacionMunicipioProvincia> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: api/<RelacionMunicipiosProvinciasController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<string>>> GetProvincias()
        {
            try
            {
                var result = await _context.AllItems();
                return Ok(result.Select(p => p.Provincia).Distinct().ToList());
            }catch(Exception exc)
            {
                Debug.WriteLine(exc.Message);
                throw;
            }
        }

        // GET api/<RelacionMunicipiosProvinciasController>/5
        [HttpGet("{provincia}")]
        public async Task<ActionResult<IEnumerable<string>>> GetMunicipios(string provincia)
        {
            var result = await _context.AllItems();
            if (result != null)
            {
                return Ok(result.Where(p => p.Provincia.Equals(provincia)).Select(p => p.Nombre).Distinct().ToList());
            }
            return NoContent();
        }       
    }
}
