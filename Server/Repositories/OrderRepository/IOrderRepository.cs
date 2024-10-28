// Repositories/Interfaces/IOrderRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;
namespace Server.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrderByUserIdAsync(int userId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
