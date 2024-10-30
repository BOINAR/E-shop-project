using Server.Models;


namespace Server.Repositories.OrderItemRepository
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> GetOrderItemById(int id);
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task AddOrderItemAsync(OrderItem orderItem);

        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemByIdAsync(int orderId);
    }
}

