using System;
using System.Collections.Generic;
using System.Text;

namespace Outthink_VendingMachine_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
