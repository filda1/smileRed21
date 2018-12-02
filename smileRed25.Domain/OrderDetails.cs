using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smileRed25.Domain
{
    public class OrderDetails
    {    
        public int OrderDetailsID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public bool VisibleOrderDetails { get; set; }
        public bool ActiveOrderDetails { get; set; }
        public string Ingredients { get; set; }
    }
}