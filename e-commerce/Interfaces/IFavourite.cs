using e_commerce.Entities;

namespace e_commerce.Interfaces
{
    public interface IFavourite
    {
        public Favourite GetUserFav(string userId);
        public FavouriteItems IfExist(Favourite favourite, int productId);
        public void AddItem(FavouriteItems product);

        public void RemoveItem(FavouriteItems item);


    }
}
