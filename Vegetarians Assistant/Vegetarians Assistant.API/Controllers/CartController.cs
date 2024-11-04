using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Customer;
using Vegetarians_Assistant.Services.Services.Interface.ICart;
using Vegetarians_Assistant.Services.Services.Interface.Membership;

namespace Vegetarians_Assistant.API.Controllers
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

        [HttpPost("/api/v1/carts/addToCart")]
        public async Task<IActionResult> addToCart([FromBody] CartInfoView view)
        {
            try
            {
                await _cartService.addToCart(view);
                return StatusCode(StatusCodes.Status201Created);
            }catch (Exception ex)
            {
                return BadRequest("Thêm dish vào giỏ hàng thất bại");
            }
        }

        [HttpGet("/api/v1/carts/getCartByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<CartView>>> GetCartByUserId(int id)
        {
            var cartDetail = await _cartService.GetCartByUserId(id);
            if (cartDetail == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(cartDetail);
        }

        [HttpDelete("/api/v1/carts/removeCartByUserId/{id}")]
        public async Task<IActionResult> RemoveCartByUserId(int id)
        {
            var success = await _cartService.RemoveCartByUserId(id);

            if (success)
            {
                return Ok("Cart deleted successfully");
            }
            else
            {
                return NotFound("Cart not found or failed to delete");
            }
        }

    }
}
