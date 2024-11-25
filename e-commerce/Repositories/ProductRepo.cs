using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Entities;
using e_commerce.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace e_commerce.Repositories
{
    public class ProductRepo:IProduct
    {
        private readonly StoreContext db;

        public ProductRepo(StoreContext _db)
        {
            db = _db;
        }

        public List<Product> getAllProducts()
        {
            var Products= db.Products.ToList();
            return Products;
        }

        public Product getProductById(int id)
        {
            var spacificProduct= db.Products.FirstOrDefault(x => x.ProductId==id);
           
                return spacificProduct;
            
        }

        public Product GetProductByName(string name) 
        { return db.Products.FirstOrDefault(x => x.ProductName == name); }
        //add product
        public Product addProduct(ProductDTO productDTO,string url)
        {
            Product productModel= new Product();
            productModel.ProductName = productDTO.ProductName;
            productModel.ProductPrice = productDTO.ProductPrice;
            productModel.ProductDescription = productDTO.ProductDescription;
            productModel.ProductStock = productDTO.ProductStock;
            productModel.ProductImage = url;
            db.Products.Add(productModel);
            db.SaveChanges();
            return productModel;
        }
    }
}
