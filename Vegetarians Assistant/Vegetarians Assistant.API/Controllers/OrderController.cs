using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IOrder;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManagementService _orderManagementService;
        public OrderController(IOrderManagementService orderManagementService)
        {
            _orderManagementService = orderManagementService;
        }
        [HttpPost("/api/v1/orders/createOrderByCustomer")]
        public async Task<IActionResult> CreateOrderByCustomer([FromBody] OrderView newOrder)
        {
            bool checkOrder = await _orderManagementService.CreateOrderByCustomer(newOrder);
            if (checkOrder)
            {
                return Ok("Create success");
            }
            else
            {
                return BadRequest("Not correct role");
            }
        }
    }
}
