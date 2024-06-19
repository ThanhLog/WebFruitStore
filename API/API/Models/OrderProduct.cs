using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class OrderProduct
    {
        public int ProductID { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPricePerProduct { get; set; }
    }
}
