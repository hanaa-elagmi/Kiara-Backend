using e_commerce.Entities;

namespace e_commerce.Interfaces
{
    public interface ICart
    {
        public Cart getCart( string userId);
        public CartItems IfItemExist(int ProductId, Cart cart);

        public void SetItems(CartItems cartItem);
        public void UpdateStock(Product product);
        public void UpdateCartUser(Cart cart);
        public void UpdateQuantity(CartItems cartItem);
        public void DeleteFromCart(CartItems product);

        public void DeleteCartItem(CartItems item);
        public void Save();

    }
}
