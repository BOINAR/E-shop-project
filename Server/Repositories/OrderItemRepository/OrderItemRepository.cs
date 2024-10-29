using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Repositories.OrderItemRepository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderItemRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetOrderItemById(int id)
        {
            return await _context.OrderItems.FindAsync(id) ?? throw new Exception("OrderItem is null");
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }

}
