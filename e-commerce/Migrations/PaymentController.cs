using e_commerce.DTO;
using e_commerce.Entities;
using e_commerce.Interfaces;
using e_commerce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Migrations
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICart cartRepo;
        private readonly Ipayment paymentRepo;

        public PaymentController(ICart cartRepo, Ipayment paymentRepo)
        {
            this.cartRepo = cartRepo;
            this.paymentRepo = paymentRepo;
        }

        [HttpPost("Checkout")]
        public IActionResult Checkout(PaymentDTO payment)
        {
            var cart = cartRepo.getCart(payment.UserId);
            if (cart != null) {
                if (cart.CartItems.Count() > 0)
                {
                    Payment paymentModel = new Payment();
                    paymentModel.UserId = payment.UserId;
                    paymentModel.Address = payment.Address;
                    paymentModel.Email = payment.Email;
                    paymentModel.CardNumber = payment.CardNumber;
                    paymentModel.PhoneNumber = payment.PhoneNumber;
                    paymentModel.Name = payment.Name;
                    paymentModel.CartId = cart.CartId;
                    paymentModel.TotalPrice = cart.TotalPrice;
                    paymentModel.TotalQuantity = cart.TotalQuantity;

                    paymentRepo.Payment(paymentModel);


                    for (int i = 0; i < cart.CartItems.Count(); i++)
                    {
                        cartRepo.DeleteCartItem(cart.CartItems[i]);
                    }
                    cartRepo.Save();
                    cartRepo.UpdateCartUser(cart);

                    return Ok(new Message() { message = "payment done" });
                }
                else { return NotFound(new Message() { message = "please add products" }); }

                }

            else
                {
                    return NotFound(new Message() { message = "cart not found" });
                }


            }
        }
    } 
