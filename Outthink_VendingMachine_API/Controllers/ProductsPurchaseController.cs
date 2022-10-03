using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Outthink_VendingMachine_API.Controllers
{
    [ApiController]
    [Route("products-purchase")]
    public class ProductsPurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductsPurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: /products-purchase
        [HttpPost]
        public async Task<IActionResult> PurchaseProduct(Commands.PurchaseProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}