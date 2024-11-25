using Microsoft.AspNetCore.Identity;

namespace e_commerce.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }

    
      
    }
}
