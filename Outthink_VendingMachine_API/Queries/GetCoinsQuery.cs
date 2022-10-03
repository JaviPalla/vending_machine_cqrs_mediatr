using MediatR;
using Outthink_VendingMachine_API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outthink_VendingMachine_API.Queries
{
    public class GetCoinsQuery : IRequest<List<CoinDTO>>
    {
        public double? CoinValue { get; set; }
    }
}
