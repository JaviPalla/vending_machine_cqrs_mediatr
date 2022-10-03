using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Outthink_VendingMachine_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CoinsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /coins
        [HttpGet]
        public async Task<IActionResult> GetCoins()
        {
            var response = await _mediator.Send(new Queries.GetCoinsQuery());
            if (response == null) { return NotFound(); }
            return Ok(response);
        }

        // GET: /coins/{value}
        [HttpGet("{value}")]
        public async Task<IActionResult> GetCoinByValue(double value)
        {
            var response = await _mediator.Send(new Queries.GetCoinsQuery { CoinValue = value });
            if (response == null) { return NotFound(); }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Commands.AddCoinCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}