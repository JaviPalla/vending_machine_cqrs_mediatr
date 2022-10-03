using MediatR;
using Outthink_VendingMachine_API.Database;
using Outthink_VendingMachine_API.DTOs;
using Outthink_VendingMachine_API.Notifications;
using Outthink_VendingMachine_API.Services;

namespace Outthink_VendingMachine_API.Commands
{
    public class CancelPurchaseProductCommandHandler : IRequestHandler<CancelPurchaseProductCommand, List<CoinDTO>>
    {
        private readonly IVendingMachineDb _db;
        private readonly IMediator _mediator;
        private readonly ICoinChangeService _coinChangeService;

        public CancelPurchaseProductCommandHandler(IVendingMachineDb db, IMediator mediator, ICoinChangeService coinChangeService)
        {
            _db = db;
            _mediator = mediator;
            _coinChangeService = coinChangeService;
        }

        public async Task<List<CoinDTO>> Handle(CancelPurchaseProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var change = _coinChangeService.CalculateChange(request.InsertedCoins.Sum(), 0);
                await _mediator.Publish(new ChangeReturnedNotification { Coins = change });
                return change;
            }
            catch (global::System.Exception)
            {
                return null;
            }
        }
    }
}
