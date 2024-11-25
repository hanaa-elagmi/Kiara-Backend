using e_commerce.DTO;
using e_commerce.Entities;
using e_commerce.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IFavourite favouriteRepo;
        private readonly IProduct productRepo;

        public FavouriteController(IFavourite favouriteRepo,IProduct productRepo)
        {
            this.favouriteRepo = favouriteRepo;
            this.productRepo = productRepo;
        }

        [HttpPost("AddToFavourite")]
        public IActionResult AddToFavourite(int productId,string userId)
        {
            var item=productRepo.getProductById(productId);
            var favContainer = favouriteRepo.GetUserFav(userId);

            var checkExesting=favouriteRepo.IfExist(favContainer, productId);
            if (checkExesting != null) 
            {
                return BadRequest(new Message() {message="Product Exist" });
            }
            else
            {
                FavouriteItems favouriteItems=new FavouriteItems();
                favouriteItems.ProductId = productId;
                favouriteItems.Name=item.ProductName;
                favouriteItems.Image = item.ProductImage;
                favouriteItems.FavouriteId=favContainer.FavouriteId;
                favouriteRepo.AddItem(favouriteItems);
                return Ok(new Message() { message="Item Added To Favourites"});
            }


        }

        [HttpDelete("DeleteFromFavourite")]
        public IActionResult DeleteFromFavourite(int ItemId,string userId)
        {
            var item = productRepo.getProductById(ItemId);
            var favContainer = favouriteRepo.GetUserFav(userId);

            var checkExesting = favouriteRepo.IfExist(favContainer, ItemId);

            if (checkExesting != null) 
            {
                favouriteRepo.RemoveItem(checkExesting);
               
                return Ok(new Message() { message = "Item removed" });
            }
            else
            {
                return BadRequest(new Message() { message="Item is not Exist"});
            }
        }

        [HttpGet("GetFavourite")]
        public IActionResult GetFavourite(string userId)
        {
            var UserFav=favouriteRepo.GetUserFav(userId);
            if (UserFav != null)
            {
                return Ok(UserFav);
            }
            else
            {
                return BadRequest(new Message() { message = "No Favourites Yet" });
            }
        }
    }
}
