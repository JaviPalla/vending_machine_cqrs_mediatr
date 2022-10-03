using MediatR;
using Microsoft.EntityFrameworkCore;
using Outthink_VendingMachine_API.Database;
using Outthink_VendingMachine_API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Outthink_VendingMachine_API.Queries
{
    public class GetCoinsQueryHandler : IRequestHandler<GetCoinsQuery, List<CoinDTO>>
    {
        private readonly IVendingMachineDb _db;

        public GetCoinsQueryHandler(VendingMachineDb db)
        {
            _db = db;
        }

        public async Task<List<CoinDTO>?> Handle(GetCoinsQuery request, CancellationToken cancellationToken)
        {
            var coins = await _db.Coins.Where(p => request.CoinValue == null || p.Value == request.CoinValue).ToListAsync();
            if (coins.Count == 0) { return null; }
            return coins.Select(p => new CoinDTO
            {
                Value = p.Value,
                Quantity = p.Quantity
            }).ToList();
        }
    }
}
