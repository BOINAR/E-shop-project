using Server.Models;

namespace Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order newOrder);
        Task<Order> AddProductToOrder(int orderId, int productId);
        Task<Order> RemoveProductFromOrder(int orderId, int productId);

        Task<IEnumerable<Order>> GetUserOrders(int userId);

        void UpdateOrderTotal(Order order);



    }
}