using e_commerce.DTO;
using e_commerce.Entities;

namespace e_commerce.Interfaces
{
    public interface IProduct
    {
        public List<Product> getAllProducts();
        public Product getProductById(int id);
        public Product GetProductByName(string name);
        public Product addProduct(ProductDTO productDTO,string url);
        
    }
}
