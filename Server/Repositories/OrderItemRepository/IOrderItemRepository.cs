using Server.Models;


namespace Server.Repositories.OrderItemRepository
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> GetByIdAsync(int id);
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task AddAsync(OrderItem orderItem);
        Task UpdateAsync(OrderItem orderItem);
        Task DeleteAsync(OrderItem orderItem);
    }
}

