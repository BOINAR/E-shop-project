using Microsoft.AspNetCore.Identity.Data;
using Server.Models;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new List<Order>();

        public Order CreateOrder(Order newOrder)
        {
            // Logique pour créer une nouvelle commande
            _orders.Add(newOrder);
            return newOrder;
        }

        public Order AddProductToOrder(int orderId, int productId)
        {
            // Logique pour ajouter un produit à une commande
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // Vérifier si le produit est déjà dans la commande
            var orderItem = order.OrderItems?.FirstOrDefault(item => item.ProductId == productId);
            if (orderItem?.Quantity != null)
            {
                // Si le produit est déjà dans la commande, augmenter la quantité
                orderItem.Quantity += 1;
            }
            else
            {
                // Sinon, créer un nouvel OrderItem et l'ajouter à la commande
                var newOrderItem = new OrderItem
                {
                    ProductId = productId,
                    Quantity = 1
                };
                order.OrderItems?.Add(newOrderItem);
            }
            // Mettre à jour le total de la commande
            if (order.OrderItems != null)
            {
                order.TotalAmount = order.OrderItems.Sum(item => (item?.Products?.Price ?? 0m) * (item?.Quantity ?? 0));
            }




            return order;
        }
        public Order RemoveProductFromOrder(int orderId, int productId)
        {
            // Rechercher la commande par son Id
            var order = _orders.FirstOrDefault(o => o.Id == orderId);

            // Si la commande n'existe pas, on lève une exception
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // Rechercher l'article (OrderItem) à retirer via son ProductId
            var orderItem = order.OrderItems?.FirstOrDefault(item => item.ProductId == productId);

            // Si l'article n'est pas trouvé dans la commande, lever une exception
            if (orderItem == null)
            {
                throw new Exception("Product not found in the order");
            }

            // Si la quantité de l'article est supérieure à 1, on réduit simplement la quantité
            if (orderItem.Quantity > 1)
            {
                // Réduire la quantité de l'article dans la commande
                orderItem.Quantity--;
            }
            // Si la quantité est de 1, cela signifie qu'on doit retirer complètement l'article
            else
            {
                // Retirer l'article de la liste d'articles dans la commande
                order.OrderItems?.Remove(orderItem);
            }

            // Mettre à jour le total de la commande après la suppression ou réduction de l'article
            UpdateOrderTotal(order);

            // Retourner la commande mise à jour
            return order;
        }
        // Méthode pour recalculer le total de la commande
        public void UpdateOrderTotal(Order order)
        {
            if (order?.OrderItems != null)
            {
                decimal total = 0;
                foreach (var item in order.OrderItems)
                {
                    total += (item.Products?.Price ?? 0m) * item.Quantity;
                }
                order.TotalAmount = total;
            }
        }


    }
}