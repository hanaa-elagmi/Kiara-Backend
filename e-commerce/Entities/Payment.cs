using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; } //unique
        public string Email { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        
        public int CartId { get; set; }
       public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
