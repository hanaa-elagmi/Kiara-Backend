using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.DTO
{
    public class PaymentDTO
    {
        
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; } //unique
        public string Email { get; set; }
        public string UserId { get; set; }
      
    }
}
