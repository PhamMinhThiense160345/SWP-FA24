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

        [HttpPost("/api/v1/cart/addToCart")]
        public async Task<IActionResult> addToCart([FromBody] CartView view)
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

       
    }
}
