using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace e_commerce.Entities
{
    public class Favourite
    {
        [Key]
        [JsonIgnore]
        public int FavouriteId { get; set; }

        public List<FavouriteItems> FavouriteItems { get; set; }=new List<FavouriteItems>();

        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }

    }
}
