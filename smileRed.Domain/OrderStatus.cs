using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace smileRed.Domain
{
    public class OrderStatus
    {
        [Key]
        [Display(Name = "OrderID")]
        public int OrderStatusID { get; set; }

        [Display(Name = "Order Status")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string OrderStatusName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
