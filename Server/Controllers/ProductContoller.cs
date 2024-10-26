using Microsoft.AspNetCore.Mvc;
using Server.Models;
namespace Server.Services.ProductService
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ActionResult<Product> UpdateProduct(int productId, [FromBody] Product updatedProduct)
        {
            var product = _productService?.UpdateProduct(productId, updatedProduct);
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
