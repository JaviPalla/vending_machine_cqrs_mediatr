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
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDTO>>
    {
        private readonly IVendingMachineDb _db;

        public GetProductsQueryHandler(VendingMachineDb db)
        {
            _db = db;
        }

        public async Task<List<ProductDTO>?> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _db.Products.Where(p => request.Id == null || p.Id == request.Id).ToListAsync();
            if (products.Count == 0) { return null; }
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();
        }
    }
}
