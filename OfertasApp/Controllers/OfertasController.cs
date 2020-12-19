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
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.Conflict)]
        //[ProducesResponseType(typeof(Oferta), (int)HttpStatusCode.Created)]
        //public async Task<ActionResult<Oferta>> Post([FromBody] Oferta oferta)
        //{
        //    try
        //    {
        //        await _context.AddAsync(oferta);
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (OfertaExists(oferta.IdOferta))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetOferta", new { id = oferta.IdOferta }, oferta);
        //}

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

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.Accepted)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        //{
        //    // get total price of the basket
        //    // remove the basket 
        //    // send checkout event to rabbitMq 

        //    var basket = await _repository.GetBasket(basketCheckout.UserName);
        //    if (basket == null)
        //    {
        //        _logger.LogError("Basket not exist with this user : {EventId}", basketCheckout.UserName);
        //        return BadRequest();
        //    }

        //    var basketRemoved = await _repository.DeleteBasket(basketCheckout.UserName);
        //    if (!basketRemoved)
        //    {
        //        _logger.LogError("Basket can not deleted");
        //        return BadRequest();
        //    }

        //    // Once basket is checkout, sends an integration event to
        //    // ordering.api to convert basket to order and proceeds with
        //    // order creation process

        //    var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        //    eventMessage.RequestId = Guid.NewGuid();
        //    eventMessage.TotalPrice = basket.TotalPrice;

        //    try
        //    {
        //        _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.RequestId, "Basket");
        //        throw;
        //    }

        //    return Accepted();
        //}
        //private readonly IMediator _mediatr;

        //public OfertasController(IMediator mediatr)
        //{
        //    _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        //}

        // GET: api/Personas
        
        [HttpGet("GetOfertaByTitulo/{titulo}")]
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
