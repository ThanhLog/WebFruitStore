using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; }

        public string Image { get; set; }

        public string ProductName { get; set; }

        public string Origin { get; set; }

        public decimal Price { get; set; }

        public int Sold { get; set; }

        public int Stock { get; set; }

        public int Imported { get; set; }
    }
}
