using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_commerce.Entities
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [JsonIgnore]
        public List<CartItems> CartItems { get; set; }=new List<CartItems>();

       

    }
}
