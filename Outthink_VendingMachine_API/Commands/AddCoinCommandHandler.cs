using MediatR;
using Outthink_VendingMachine_API.Database;
using System.Linq;

namespace Outthink_VendingMachine_API.Commands
{
    public class AddCoinCommandHandler : IRequestHandler<AddCoinCommand, bool>
    {
        private readonly IVendingMachineDb _db;
        private readonly IMediator _mediator;

        public AddCoinCommandHandler(IVendingMachineDb db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        private readonly List<double> SUPPORTED_COINS = new List<double> { 0.10, 0.2, 0.5, 1.00 };

		public async Task<bool> Handle(AddCoinCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Add new coin to coin list
                var coin = _db.Coins.FirstOrDefault(c => c.Value == request.Value);
                if (coin == null) { throw new Exception($"Coin not supported. Supported coins are {String.Join(", ", SUPPORTED_COINS)}"); }
                coin.Quantity += 1;
                await _db.SaveChangesAsync(cancellationToken);

                // Send notification
                //await _mediator.Publish(new CoinInserted(coin));

                return true;
            }
            catch (global::System.Exception)
            {
                return false;
            }
        }
    }
}
