// Repositories/OrderRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id) ?? throw new Exception("Order is null");
        }

        public async Task<IEnumerable<Order>> GetOrderByUserIdAsync(int userId)
        {
            return await _context.Orders.Where(order => order.Id == userId).ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
