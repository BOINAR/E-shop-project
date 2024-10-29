using Server.Models;


namespace Server.Repositories.OrderItemRepository
{
    public interface IOrderItemRepository { Task<OrderItem> GetOrderItemById(int id); 
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(int orderId); 
    Task AddOrderItem(OrderItem orderItem); Task UpdateOrderItem(OrderItem orderItem); 
    Task DeleteOrderItem(int id); }
}

