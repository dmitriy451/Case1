using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
    public class Rootobject
    {
        public Drink[] Property1 { get; set; }
    }
    public class Drink
    {

        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public Byte[] Image { get; set; }
        public float Cost { get; set; }


    }
}
