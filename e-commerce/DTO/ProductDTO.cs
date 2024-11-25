using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace e_commerce.DTO
{
    public class ProductDTO
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int ProductStock { get; set; }
        public IFormFile ProductImage { get; set; }

    }
}