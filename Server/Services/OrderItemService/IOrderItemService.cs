using Server.Models;

namespace Server.Services.OrderItemService
{
    public interface IOrderItemService
    {
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(int id, OrderItem updatedOrderItem);
        Task DeleteOrderItemAsync(int id);
    }
}