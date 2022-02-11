using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
    internal class Coins
    {
            public int Id { get; set; }
            public int VendingMachineId { get; set; }
            public int Denomination { get; set; }
            public int Count { get; set; }
            public int IsActive { get; set; }
    }
}
