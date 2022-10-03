using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Outthink_VendingMachine_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _mediator.Send(new Outthink_VendingMachine_API.Queries.GetProductsQuery());
            if (response == null) { return NotFound(); }
            return Ok(response);
        }

        // GET: /products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _mediator.Send(new Outthink_VendingMachine_API.Queries.GetProductsQuery { Id = id });
            if (response == null) { return NotFound(); }
            return Ok(response);
        }
    }
}