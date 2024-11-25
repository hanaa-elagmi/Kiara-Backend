using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly StoreContext Store;
        private readonly IConfiguration config;

        //we inject user manger ==> help me to add user to db
        public AccountController(UserManager<ApplicationUser>userManager,StoreContext store_context,IConfiguration config)
        {
            this.userManager = userManager;
            Store = store_context;
            this.config = config;
        }

        #region register

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if(ModelState.IsValid)
            {
                //make sure that the email & userName are unique
                ApplicationUser userEmail=await userManager.FindByEmailAsync(registerDto.Email);
                if (userEmail != null)
                {
                    return BadRequest(new Message(){ message = "Email Already Exist" });
                }
                ApplicationUser userName=await userManager.FindByNameAsync(registerDto.UserName);
                if (userName != null)
                {
                    return BadRequest(new Message() { message = "UserName Already Exist" });
                }
                //create
                ApplicationUser UserModel = new ApplicationUser();
                UserModel.Email = registerDto.Email;
                UserModel.Name = registerDto.Name;
                UserModel.PhoneNumber = registerDto.PhoneNumber;
                UserModel.UserName = registerDto.UserName;
                IdentityResult result=await userManager.CreateAsync(UserModel, registerDto.Password);
                if (result.Succeeded)
                {
                    Favourite favouriteContainer=new Favourite() { UserId=UserModel.Id,FavouriteItems=new List<FavouriteItems>()};
                    Cart userCart = new Cart() { UserId = UserModel.Id, TotalPrice = 0, TotalQuantity = 0, CartItems = new List<CartItems>()};
                    Store.Carts.Add(userCart);
                    Store.Favorite.Add(favouriteContainer);
                    Store.SaveChanges();
                    
                    return Ok(new Message(){ message = "Account Created" });
                }
                else
                {
                    //to return all errors 
                    var ErrorList=new List<IdentityError>();
                    foreach (var error in result.Errors)
                    {
                        ErrorList.Add(error);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        #endregion register

        #region login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (ModelState.IsValid)
            {
                //check
                ApplicationUser userModel=await userManager.FindByEmailAsync(loginDto.Email);
                if (userModel != null)
                {
                    if (await userManager.CheckPasswordAsync(userModel,loginDto.Password))
                    {
                        //claims
                        List<Claim> myclaims = new List<Claim>();
                        myclaims.Add(new Claim("UserId" , userModel.Id));
                        myclaims.Add(new Claim("UserName", userModel.UserName));
                        myclaims.Add(new Claim("Name", userModel.Name));
                        myclaims.Add(new Claim("PhoneNumber", userModel.PhoneNumber));
                        myclaims.Add(new Claim("Email", userModel.Email));
                        myclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var authSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));
                        SigningCredentials credentials = new SigningCredentials(authSecurityKey,SecurityAlgorithms.HmacSha256);
                        //create token
                        JwtSecurityToken mytoken= new JwtSecurityToken(
                            issuer: config["JWT:ValidIss"],
                            audience: config["JWT:ValidAud"],
                            expires:DateTime.Now.AddHours(2),
                            claims:myclaims,
                            signingCredentials:credentials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        }); 
                    }
                    else { return BadRequest(new Message() { message="Invalid Password"}); }
                }
                else { return BadRequest(new Message() { message="Email Not Found"}); }
               
            }
            else { return BadRequest(ModelState); }
        }
        #endregion login
    }
}
