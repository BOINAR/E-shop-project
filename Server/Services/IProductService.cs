using Server.Models;

namespace Server.Services
{
    public interface IProductService
    {

        IEnumerable<Product> GetAllProduct();
        Product GetProductById(int productId);
        Product CreateProduct(Product newProduct);
        Product UpdateProduct(int productId, Product updatedProduct, string newName, decimal newPrice, string newDescription);
        void DeleteProduct(int ProductId);

    }
}