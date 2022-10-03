using MediatR;

namespace Outthink_VendingMachine_API.Notifications
{
    public class ProductSoldNotification : INotification
    {
        public int Id { get; set; }
    }
}
