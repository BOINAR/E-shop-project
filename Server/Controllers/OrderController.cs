using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.OrderService;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService? _OrderService;

        public OrderController(IOrderService orderService)
        {
            _OrderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order newOrder)
        {

            if (_OrderService == null) { throw new Exception("_OrderService is null"); }
            var order = await _OrderService.CreateOrder(newOrder);
            return Ok(newOrder);
        }

        [HttpPost("{orderId}/products/{productId}")]
        public async Task<ActionResult<Order>> AddProductFromOrder(int orderId, int productId)
        {
            if (_OrderService == null) { throw new Exception("_OrderService is null"); }
            var order = await _OrderService.AddProductToOrder(orderId, productId);
            return Ok(order);
        }

        [HttpDelete("{orderId}/products/{productId}")]
        public async Task<ActionResult<Order>> RemoveFromOrder(int orderId, int productId)
        {
            if (_OrderService == null){
                throw new Exception("orderservice is null");
            }
            var order = await _OrderService.RemoveProductFromOrder(orderId, productId);
            return Ok(order);
        }

        [HttpGet("{UserId}")]
        public ActionResult<Order> GetUsersOrders(int UserId)
        {
            var orders = _OrderService?.GetUserOrders(UserId);
            return Ok(orders);
        }
    }
}