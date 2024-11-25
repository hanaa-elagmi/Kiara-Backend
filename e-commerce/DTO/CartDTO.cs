namespace e_commerce.DTO
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public string UserId { get; set; }

        public List<CartItemsDTO> Items { get; set; }=new List<CartItemsDTO>();
    }
}
