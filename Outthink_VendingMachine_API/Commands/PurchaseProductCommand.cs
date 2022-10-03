using MediatR;
using Outthink_VendingMachine_API.DTOs;

namespace Outthink_VendingMachine_API.Commands
{
    public class PurchaseProductCommand : IRequest<ProductPurchaseResult>
    {
        public int ProductId { get; set; }
        public List<double> InsertedCoins { get; set; }
    }
}
