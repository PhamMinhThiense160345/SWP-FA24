using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        [HttpGet("/api/v1/orders/getOrderByStatus/{Status}")]
        public async Task<ActionResult<IEnumerable<OrderView>>> GetOrderByStatus(string Status)
        {
            var orderDetail = await _orderManagementService.GetOrderByStatus(Status);
            if (orderDetail == null)
            {
                return NotFound("Order not found");
            }
            return Ok(orderDetail);
        }
        [HttpGet("/api/v1/orders/allOrder")]
        public async Task<ActionResult<IEnumerable<OrderView>>> AllOrder()
        {

            var ordersList = await _orderManagementService.GetAllOrder();
            if (ordersList.IsNullOrEmpty())
            {
                return NotFound("No orders found on the system");
            }
            return Ok(ordersList);
        }
        [HttpPut("/api/v1/orders/{id}/change-status")]
        public async Task<IActionResult> ChangeOrderStatus(int id, [FromBody] string newStatus)
        {
            try
            {
                var success = await _orderManagementService.ChangeOrderStatus(id, newStatus);

                if (success)
                {
                    return Ok("Order status updated successfully");
                }
                else
                {
                    return NotFound("Order not found or failed to update status");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
