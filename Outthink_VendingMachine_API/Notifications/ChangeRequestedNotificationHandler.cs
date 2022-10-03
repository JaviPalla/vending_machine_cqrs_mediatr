using MediatR;
using Outthink_VendingMachine_API.Database;

namespace Outthink_VendingMachine_API.Notifications
{
    public class ChangeReturnedNotificationHandler : INotificationHandler<ChangeReturnedNotification>
    {
        private readonly IVendingMachineDb _db;

        public ChangeReturnedNotificationHandler(IVendingMachineDb db)
        {
            _db = db;
        }

        public Task Handle(ChangeReturnedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.Coins == null || notification.Coins.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var coin in notification.Coins)
            {
                var coinInDb = _db.Coins.FirstOrDefault(c => c.Value == coin.Value);
                if (coinInDb != null)
                {
                    coinInDb.Quantity -= 1;
                }
            }
            return _db.SaveChangesAsync(cancellationToken);
        }
    }
}
