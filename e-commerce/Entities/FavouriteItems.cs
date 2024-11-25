using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_commerce.Entities
{
    public class FavouriteItems
    {
        [ForeignKey("Favourite")]
        public int FavouriteId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Image {  get; set; }
        [JsonIgnore]
        public Favourite Favourite { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

    }
}
