using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IReadOnlyList<string>>> GetProvincias()
        {
            try
            {
                var result = await _context.GetAllAsync();
                return Ok(result.Select(p => p.Provincia).Distinct().ToList());
            }
            catch(Exception exc)
            {
                Debug.WriteLine(exc.Message);
                throw;
            }
        }

        // GET api/<RelacionMunicipiosProvinciasController>/5
        [HttpGet("{provincia}")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetMunicipios(string provincia)
        {
            var result = await _context.GetAllAsync();
            if (result != null)
            {
                return Ok(result.Where(p => p.Provincia.Equals(provincia)).Select(p => p.Nombre).Distinct().ToList());
            }
            return NoContent();
        }       
    }
}
