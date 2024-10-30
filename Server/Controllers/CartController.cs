using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Server.Models;
using Server.Services.CartService;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/Cart/userId
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        // POST: api/Cart/AddItem
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItemToCart([FromBody] CartItem cartItem, int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
                return NotFound("Cart not found for the user.");

            await _cartService.AddItemToCart(cart, cartItem);
            return Ok(cart);
        }

        // PUT: api/Cart/UpdateItem
        [HttpPut("UpdateItem/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int userId, int cartItemId, [FromBody] CartItem updatedCartItem)
        {
            var result = await _cartService.UpdateCartItem(userId, cartItemId, updatedCartItem);
            if (!result)
                return NotFound("Cart item not found.");

            return Ok("Cart item updated successfully.");
        }

        // DELETE: api/Cart/RemoveItem/5
        [HttpDelete("RemoveItem/{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            var result = await _cartService.RemoveItemFromCart(cartItemId);
            if (!result)
                return NotFound("Cart item not found.");

            return Ok("Cart item removed successfully.");
        }
    }
}