using MediatR;
using Outthink_VendingMachine_API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outthink_VendingMachine_API.Queries
{
    public class GetProductsQuery : IRequest<List<ProductDTO>>
    {
        public int? Id { get; set; }
    }
}
