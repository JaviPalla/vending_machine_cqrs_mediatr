using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Outthink_VendingMachine_API.Models
{

    public class Coin
    {
        [Key]
        public double Value { get; set; }
        public int Quantity { get; set; }
    }
}
