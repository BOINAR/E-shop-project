using Server.Models;

namespace Server.Services
{
    public interface IOrderService
    {
        Order CreateOrder(Order newOrder);
        Order AddProductToOrder(int orderId, int productId);
        Order RemoveProductFromOrder(int orderId, int productId);

         void UpdateOrderTotal(Order order); 



    }
}