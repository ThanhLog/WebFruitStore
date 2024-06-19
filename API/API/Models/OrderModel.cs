using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderID { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public string FullName { get; set; }
        public List<OrderProduct> Products { get; set; } = new List<OrderProduct>();
        public decimal TotalPriceOfOrder { get; set; }
        public string Status { get; set; }
    }
}
