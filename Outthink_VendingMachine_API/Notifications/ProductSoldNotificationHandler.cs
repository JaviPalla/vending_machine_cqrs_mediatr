using MediatR;
using Outthink_VendingMachine_API.Database;
using System.Linq;

namespace Outthink_VendingMachine_API.Notifications
{
    public class ProductSoldNotificationHandler : INotificationHandler<ProductSoldNotification>
    {
        private readonly IVendingMachineDb _db;
        private readonly IMediator _mediator;

        public ProductSoldNotificationHandler(IVendingMachineDb db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        public Task Handle(ProductSoldNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                // Add new coin to coin list
                var product = _db.Products.FirstOrDefault(p => p.Id == notification.Id);
                if (product == null) { throw new Exception($"Product not found"); }
                if (product.Quantity == 0) { throw new Exception($"Product is out of stock"); }
                product.Quantity -= 1;

                return _db.SaveChangesAsync(cancellationToken); ;
            }
            catch (global::System.Exception)
            {
                return Task.CompletedTask;
            }
        }
    }
}
