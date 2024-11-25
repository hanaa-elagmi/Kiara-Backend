using e_commerce.Context;
using e_commerce.Entities;
using e_commerce.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories
{
    public class FavouriteRepo:IFavourite
    {
        private readonly StoreContext db;

        public FavouriteRepo(StoreContext db)
        {
            this.db = db;
        }

        public void AddItem (FavouriteItems product)
        {
            db.FavoriteItems.Add(product);
            db.SaveChanges();
        }
        public void RemoveItem(FavouriteItems item) 
        {
            db.FavoriteItems.Remove(item);
            db.SaveChanges();
        }

        public Favourite GetUserFav(string userId)
        {
            return db.Favorite.Include(f=>f.FavouriteItems).FirstOrDefault(f => f.UserId == userId);
        }

        public FavouriteItems IfExist(Favourite favourite,int productId)
        {
            var item= favourite.FavouriteItems.FirstOrDefault(f => f.ProductId == productId);
            if (item == null) { return null; }
            else { return item; }
        }
    }
}
