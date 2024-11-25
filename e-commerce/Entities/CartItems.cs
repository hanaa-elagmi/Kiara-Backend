using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Entities
{
    public class CartItems
    {
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice => Price * Quantity;
        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }


    }
}
