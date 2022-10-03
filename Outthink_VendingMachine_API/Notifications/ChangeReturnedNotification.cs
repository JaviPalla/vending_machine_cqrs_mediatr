using MediatR;
using Outthink_VendingMachine_API.DTOs;

namespace Outthink_VendingMachine_API.Notifications
{
    public class ChangeReturnedNotification : INotification
    {
        public List<CoinDTO> Coins { get; set; }
    }
}
