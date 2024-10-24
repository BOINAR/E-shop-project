using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.OrderService;

namespace Server.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService? _OrderService;

        public OrderController(IOrderService orderService)
        {
            _OrderService = orderService;
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order newOrder)
        {

            if (_OrderService == null) { throw new Exception("_OrderService is null"); }
            var order = _OrderService.CreateOrder(newOrder);
            return Ok(newOrder);
        }

        [HttpPost("{orderId}/products/{productId}")]
        public ActionResult<Order> AddProductFromOrder(int orderId, int productId)
        {
            if (_OrderService == null) { throw new Exception("_OrderService is null"); }
            var order = _OrderService.AddProductToOrder(orderId, productId);
            return Ok(order);
        }

        [HttpDelete("{orderId}/products/{productId}")]
public ActionResult<Order> RemoveFromOrder(int orderId, int productId){
    var order = _OrderService?.RemoveProductFromOrder(orderId,productId);
    return Ok(order);
}

[HttpGet("{UserId}")]
public ActionResult<Order> GetUsersOrders(int UserId){
var orders = _OrderService?.GetUserOrders(UserId);
return Ok(orders);
}
    }
}