using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smileRed25.Domain
{
    public class Order
    {
        public int OrderID { get; set; }    
        public int OrderStatusID { get; set; }
        public int UserId { get; set; }      
        public string Email { get; set; }
        public DateTime DateOrder { get; set; }
        public bool Delete { get; set; }    
        public bool VisibleOrders { get; set; }   
        public bool ActiveOrders { get; set; }       
    }
}