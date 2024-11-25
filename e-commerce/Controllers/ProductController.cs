using e_commerce.DTO;
using e_commerce.Entities;
using e_commerce.Interfaces;
using e_commerce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productrepo;
        public ProductController(IProduct productrepo)
        {
            this.productrepo = productrepo;
        }


        //add
        [HttpPost("addProduct")]
        public IActionResult AddProduct( ProductDTO product)
        {
            var projectFolder = Directory.GetCurrentDirectory();
            var relativeImagesPath = Path.Combine("wwwroot", "Images");
            var fullImagesPath = Path.Combine(projectFolder, relativeImagesPath);
            var fileName = $"{Guid.NewGuid()}_{product.ProductImage.FileName}";
            var fullImagePath = Path.Combine(fullImagesPath, fileName);
            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                product.ProductImage.CopyTo(stream);
                stream.Flush();
            }
            var url = $"{Request.Scheme}://{Request.Host}/wwwroot/Images/{fileName}";

            var myproduct = productrepo.addProduct(product, url);




            return Ok(myproduct);

        }
        //get all
        [HttpGet("getAllProducts")]
        public IActionResult GetAllProducts()
        {
            var all=productrepo.getAllProducts();
            return Ok(all);
        }

        //getby id
        [HttpGet("getProductById")]

        public IActionResult GetProduct(int id) 
        {
            var product=productrepo.getProductById(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound(new Message() { message="Product Not Found"});
            }
        }

        //get by name
        [HttpGet("getProductByName")]
        public IActionResult getByName(string name) 
        {
        var product=productrepo.GetProductByName(name);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound(new Message() { message = "Product Not Found" });
            }

        }

        //pagination

        [HttpGet("getProductByPage/{page}")]
        public IActionResult getProductByPage(int page)
        {
            if(page < 1)
            {
                page = 1;
            }

            var totalProducts=productrepo.getAllProducts().Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)8);

            if (page > totalPages)
            {
                page = totalPages;
            }

            var products = productrepo.getAllProducts().Skip((page-1)*8).Take(8).ToList();
            return Ok(new {pages=totalPages,products=products,page=page});

        }
    }
}
