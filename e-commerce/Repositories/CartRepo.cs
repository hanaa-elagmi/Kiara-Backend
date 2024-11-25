using e_commerce.Context;
using e_commerce.Entities;
using e_commerce.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories
{
    public class CartRepo : ICart
    {
        private readonly StoreContext db;

        public CartRepo(StoreContext db)
        {
            this.db = db;
        }

        public Cart getCart(string userId)
        {
            var cartUser = db.Carts.Include(c => c.CartItems).FirstOrDefault(x => x.UserId == userId);
            return cartUser;
        }

        public CartItems IfItemExist(int ProductId,Cart cart )
        {
            var Item = cart.CartItems.FirstOrDefault(x => x.ProductId == ProductId);
             if (Item != null)
            {
                return Item;
            }
            return null;
        }

        public  void SetItems(CartItems cartItem )
        {
            db.CartItems.Add(cartItem);
            db.SaveChanges();
        }

        public void UpdateStock(Product product) 
        { 
            db.Update(product);
            db.SaveChanges();
        }
        public void UpdateQuantity(CartItems cartItem)
        {
            db.CartItems.Update(cartItem);
            db.SaveChanges();
        }
        public void UpdateCartUser(Cart cart)
        {
            cart.TotalPrice=cart.CartItems.Sum(x => x.TotalPrice);
            cart.TotalQuantity=cart.CartItems.Sum(x=>x.Quantity);
            db.Carts.Update(cart);
            db.SaveChanges();
        }

        public void DeleteFromCart(CartItems product)
        {
            if (product.Quantity > 1)
            {
                product.Quantity--;
                db.CartItems.Update(product);
                db.SaveChanges();

            }
            else
            {
                db.CartItems.Remove(product);
                db.SaveChanges();
            }
        }

       
        public void DeleteCartItem(CartItems item)
        {
            db.CartItems.Remove(item);
           
        }
        
        public void Save() { db.SaveChanges(); }
    }
}
