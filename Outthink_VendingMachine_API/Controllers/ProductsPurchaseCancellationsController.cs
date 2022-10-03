using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Outthink_VendingMachine_API.Controllers
{
    [ApiController]
    [Route("products-purchase-cancellation")]
    public class ProductsPurchaseCancellationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductsPurchaseCancellationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: /products-purchase-cancellation
        [HttpPost]
        public async Task<IActionResult> CancelPurchase(Commands.CancelPurchaseProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}