using MediatR;
using Outthink_VendingMachine_API.DTOs;

namespace Outthink_VendingMachine_API.Commands
{
    public class CancelPurchaseProductCommand : IRequest<List<CoinDTO>>
    {
        public List<double> InsertedCoins { get; set; }
    }
}
