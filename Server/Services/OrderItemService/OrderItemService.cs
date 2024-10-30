using Server.Models;
using Server.Repositories.OrderItemRepository;

namespace Server.Services.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _orderItemRepository.GetOrderItemById(id)
                   ?? throw new Exception("OrderItem not found");
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _orderItemRepository.GetAllAsync();
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _orderItemRepository.AddOrderItemAsync(orderItem);
        }

        public async Task UpdateOrderItemAsync(int id, OrderItem updatedOrderItem)
        {
            var existingOrderItem = await _orderItemRepository.GetOrderItemById(id);
            if (existingOrderItem == null)
            {
                throw new Exception("OrderItem not found");
            }

            existingOrderItem.Quantity = updatedOrderItem.Quantity;
            existingOrderItem.UnitPrice = updatedOrderItem.UnitPrice;
            // Mettez à jour d'autres propriétés si nécessaire

            await _orderItemRepository.UpdateOrderItemAsync(existingOrderItem);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetOrderItemById(id);
            if (orderItem == null)
            {
                throw new Exception("OrderItem not found");
            }

            await _orderItemRepository.DeleteOrderItemByIdAsync(id);
        }
    }
}