namespace API.Models
{
    public class CartModel
    {
        public string UserName { get; set; }
        public string ProductID { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public int Quantity {  get; set; }
        public string Price { get; set; }
        public string Origin {  get; set; }
    }
}
