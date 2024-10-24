using Microsoft.AspNetCore.Mvc;
using Server.Models;
namespace Server.Services.ProductService
{
    public class ProductController : ControllerBase
    {

        private readonly IProductService? _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProduct()
        {
            var product = _productService?.GetAllProduct();
            return Ok(product);
        }

        [HttpGet("productId")]
        public ActionResult<Product> GetProductById(int productId)
        {
            var product = _productService?.GetProductById(productId);
            return Ok(product);
        }
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product newProduct)
        {
            var product = _productService?.CreateProduct(newProduct);
            return Ok(product);
        }

        [HttpPut("{productId}")]
        public ActionResult<Product> UpdateProduct(int productId, Product updatedProduct, string newName, decimal newPrice, string newDescription)
        {
            var product = _productService?.UpdateProduct(productId, updatedProduct, newName, newPrice, newDescription);
            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            _productService?.DeleteProduct(productId);
            return NoContent();
        }

    }
}
