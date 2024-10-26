using Server.Models;

namespace Server.Services.ProductService

{
    public interface IProductService
    {

        IEnumerable<Product> GetAllProduct();
        Product GetProductById(int productId);
        Product CreateProduct(Product newProduct);
        Product UpdateProduct(int productId, Product updatedProduct);
        void DeleteProduct(int ProductId);

    }
}