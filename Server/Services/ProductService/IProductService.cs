using Server.Models;

namespace Server.Services.ProductService

{
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}