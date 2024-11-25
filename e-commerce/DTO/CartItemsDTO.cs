using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.DTO
{
    public class CartItemsDTO
    {
       
        public int CartId { get; set; }
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
