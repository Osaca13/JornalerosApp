using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfertasApp.Commands;
using OfertasApp.Queries;
using OfertasApp.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace JornalerosApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {        
        private readonly IMediator _mediatr;

        public OfertasController(IMediator mediatr)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }

        // GET: api/Personas
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<OfertaResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<OfertaResponse>>> GetOfertaByTitulo(string titulo)
        {
            var query = new OfertaQueries(titulo);
            var ofertas = await _mediatr.Send(query);
            return Ok(ofertas);
        }

        // POST: api/Personas
        [HttpPost]
        [ProducesResponseType(typeof(OfertaResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OfertaResponse>> CheckOutOferta([FromBody] CheckOutOfertaCommand checkOutOfertaCommand)
        {            
            var oferta = await _mediatr.Send(checkOutOfertaCommand);
            return Ok(oferta);
        }
    }
}
