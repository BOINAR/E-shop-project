using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.OrderItemService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        // GET: api/OrderItem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound("OrderItem not found");
            }
            return Ok(orderItem);
        }

        // GET: api/OrderItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        // POST: api/OrderItem
        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
        {
            await _orderItemService.AddOrderItemAsync(orderItem);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.Id }, orderItem);
        }

        // PUT: api/OrderItem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem updatedOrderItem)
        {
            if (id != updatedOrderItem.Id)
            {
                return BadRequest("OrderItem ID mismatch");
            }

            await _orderItemService.UpdateOrderItemAsync(id, updatedOrderItem);
            if (updatedOrderItem == null)
            {
                return NotFound("OrderItem not found");
            }

            return NoContent();
        }

        // DELETE: api/OrderItem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            await _orderItemService.DeleteOrderItemAsync(id);
            return NoContent();
        }
    }
}