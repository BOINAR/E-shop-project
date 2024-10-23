using Server.Models;

namespace Server.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> GetAllProduct()
        {
            return _products;
        }

        public Product GetProductById(int productId)
        {
            if (_products == null)
            {
                throw new Exception("Product list is null");
            }
            return _products.FirstOrDefault(p => p.Id == productId)
                       ?? throw new Exception("Product not found");
        }

        public Product CreateProduct(Product newProduct)
        {
            _products.Add(newProduct);
            return newProduct;
        }

        public Product UpdateProduct(int productId, Product updatedProduct, string newName, decimal newPrice, string newDescription)
        {
            var product = GetProductById(productId);
            if (product == null)
            {

                throw new Exception("Product not found");
            }

            // Mettre à jour les champs du produit
            product.Name = newName;
            product.Price = newPrice;
            product.Description = newDescription;

            // Retourner le produite mis à jour
            return product;
        }

        public void DeleteProduct(int productId)
        {
            var product = GetProductById(productId);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

    }

}