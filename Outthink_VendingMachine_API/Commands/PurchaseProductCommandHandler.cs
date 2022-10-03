using MediatR;
using Outthink_VendingMachine_API.Database;
using Outthink_VendingMachine_API.DTOs;
using Outthink_VendingMachine_API.Notifications;
using Outthink_VendingMachine_API.Services;

namespace Outthink_VendingMachine_API.Commands
{
    public class PurchaseProductCommandHandler : IRequestHandler<PurchaseProductCommand, ProductPurchaseResult>
    {
        private readonly IVendingMachineDb _db;
        private readonly IMediator _mediator;
        private readonly ICoinChangeService _coinChangeService;

        public PurchaseProductCommandHandler(IVendingMachineDb db, IMediator mediator, ICoinChangeService coinChangeService)
        {
            _db = db;
            _mediator = mediator;
            _coinChangeService = coinChangeService;
        }

        public async Task<ProductPurchaseResult> Handle(PurchaseProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _db.Products.FirstOrDefault(p => p.Id == request.ProductId);
                if (product == null) { throw new Exception($"Product not found"); }
                if (product.Quantity == 0) { throw new Exception($"Product is out of stock"); }
                var paidAmount = request.InsertedCoins.Sum();
                if (product.Price > paidAmount) { throw new Exception($"Insufficient amount"); }
                var change = _coinChangeService.CalculateChange(paidAmount, product.Price);
                await _mediator.Publish(new ChangeReturnedNotification { Coins = change });
                await _mediator.Publish(new ProductSoldNotification { Id = product.Id });

                return new ProductPurchaseResult
                {
                    Change = change,
                    InfoMsg = "Thank you"
                };
            }
            catch (global::System.Exception e)
            {
                return new ProductPurchaseResult
                {
                    Change = new List<CoinDTO>(),
                    InfoMsg = e.Message
                };
            }
        }
    }
}
