using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<string>>> GetProvincias()
        {
            var result = await _context.AllItems();
            if(result!= null)
            {
               return Ok(result.Select(p => p.Provincia).Distinct().ToList());
            }
            return NoContent(); 
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
