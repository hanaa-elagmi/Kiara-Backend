using e_commerce.DTO;
using e_commerce.Entities;
using e_commerce.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IProduct productRepo;
        private readonly ICart cartRepo;

        public CartController(IProduct productRepo, ICart cartRepo)
        {
            this.productRepo = productRepo;
            this.cartRepo = cartRepo;
        }

        [HttpPost("addToCart")]
        public IActionResult AddToCart(string userId, int productId)
        {
            var product = productRepo.getProductById(productId);

            var cartUser = cartRepo.getCart(userId);
            var CheckItemExisting = cartRepo.IfItemExist(productId, cartUser);
            if (product.ProductStock == 0)
            {
                return NotFound(new Message() { message = "Stock is empity" });
            }
            if (CheckItemExisting != null)
            {
                product.ProductStock--;
                CheckItemExisting.Quantity += 1;
                cartRepo.UpdateStock(product);
                cartRepo.UpdateQuantity(CheckItemExisting);
                cartRepo.UpdateCartUser(cartUser);
            }
            else
            {
                CartItems cartItem = new CartItems();
                cartItem.ProductId = productId;
                cartItem.CartId = cartUser.CartId;
                cartItem.ProductName = product.ProductName;
                cartItem.ProductImage = product.ProductImage;
                cartItem.Price = product.ProductPrice;
                cartItem.Quantity = 1;
                product.ProductStock--;
                cartRepo.SetItems(cartItem);
                cartRepo.UpdateStock(product);
                cartRepo.UpdateCartUser(cartUser);


            }
            return Ok(new Message() { message = "Item Added" });
        }

        [HttpDelete("deleteFromCart")]
        public IActionResult DeleteFromCart(string userId, int productId)
        {
            var product = productRepo.getProductById(productId);
            var cartUser = cartRepo.getCart(userId);
            var checkItemExciting = cartRepo.IfItemExist(productId, cartUser);
            if (checkItemExciting != null)
            {
                cartRepo.DeleteFromCart(checkItemExciting);
                product.ProductStock++;
                cartRepo.UpdateStock(product);
                cartRepo.UpdateCartUser(cartUser);
                //cartRepo.UpdateQuantity(checkItemExciting);
                return Ok(new Message() { message = "Deleted" });
            }
            return BadRequest(new Message() { message = "Not Exist" });
        }


        [HttpGet("getCartItems")]
        public IActionResult GetCartItems(string userId)
        {
            CartDTO cartDTO = new CartDTO();
            List<CartItemsDTO> itemsDTOs = new List<CartItemsDTO>();
            var cart = cartRepo.getCart(userId);
            if (cart != null)
            {
                cartDTO.CartId = cart.CartId;
                cartDTO.UserId = cart.UserId;
                cartDTO.TotalPrice = cart.TotalPrice;
                cartDTO.TotalQuantity = cart.TotalQuantity;
                foreach (var item in cart.CartItems)
                {
                    CartItemsDTO itemsDTO = new CartItemsDTO();
                    itemsDTO.CartId = item.CartId;
                    itemsDTO.ProductId = item.ProductId;
                    itemsDTO.ProductName = item.ProductName;
                    itemsDTO.Price = item.Price;
                    itemsDTO.Quantity = item.Quantity;
                    itemsDTO.ProductImage = item.ProductImage;
                    itemsDTO.TotalPrice = item.TotalPrice;

                    if (cart.TotalQuantity == 0&&itemsDTO.Quantity==0)
                    {
                        return Ok(new Message() { message = "No items yet" });
                    }
                    else
                    {
                        itemsDTOs.Add(itemsDTO);
                    }
                    
                }

                cartDTO.Items = itemsDTOs;
                return Ok(cartDTO);
            }
            
            else { return NotFound(new Message() { message = "Cart Not Found" }); }
        }

        [HttpPut("updateQuantity")]
        public IActionResult UpdateQuantity(UpdateQuantityDTO updateQuantityDTO)
        {
            var product = productRepo.getProductById(updateQuantityDTO.ProductId);
            var cart = cartRepo.getCart(updateQuantityDTO.UserId);
           
            
            var ExistingCartItem = cartRepo.IfItemExist(product.ProductId, cart);
        
            if (ExistingCartItem != null)
            {
                      
                var NewQuantity = ExistingCartItem.Quantity - updateQuantityDTO.Quantity;
                if (NewQuantity < 0 && updateQuantityDTO.Quantity > product.ProductStock)
                {
                    ExistingCartItem.Quantity = product.ProductStock+ExistingCartItem.Quantity;
                    product.ProductStock = 0;
                    
                }
                else
                {
                    product.ProductStock += NewQuantity;
                    ExistingCartItem.Quantity =updateQuantityDTO.Quantity;
                    
                    
                }

                
                cartRepo.UpdateStock(product);
                cartRepo.UpdateQuantity(ExistingCartItem);
                cartRepo.UpdateCartUser(cart);
                return Ok(cart);
            }
            else
            {
                return NotFound(new Message() { message = "Can not update this product" });

            }
        }
    }
}
