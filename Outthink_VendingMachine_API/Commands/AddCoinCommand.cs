using MediatR;

namespace Outthink_VendingMachine_API.Commands
{
    public class AddCoinCommand : IRequest<bool>
    {
        public double Value { get; set; }
    }
}
