using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Staff, Customer")]
        [HttpGet("/api/v1/orders/allOrder")]
        public async Task<ActionResult<IEnumerable<OrderView>>> AllOrder()
        {

            var ordersList = await _orderManagementService.GetAllOrder();
            if (ordersList == null || !ordersList.Any())
            {
                return NotFound("No orders found on the system");
            }
            return Ok(ordersList);
        }

        [Authorize(Roles = "Staff, Customer")]
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

        [Authorize(Roles = "Staff, Customer")]
        [HttpGet("/api/v1/orders/getOrderByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<OrderView>>> GetOrderByUserId(int id)
        {
            var orderDetail = await _orderManagementService.GetOrderByUserId(id);
            if (orderDetail == null)
            {
                return NotFound("Order not found");
            }
            return Ok(orderDetail);
        }

        [Authorize(Roles = "Staff, Customer")]
        [HttpGet("/api/v1/orders/getOrderDetailByOrderId/{id}")]
        public async Task<ActionResult<IEnumerable<OrderDetailInfo>>> GetOrderDetailOrderId(int id)
        {
            var orderDetail = await _orderManagementService.GetOrderDetailOrderId(id);
            if (orderDetail == null)
            {
                return NotFound("Order detail not found");
            }
            return Ok(orderDetail);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/orders/createOrderByCustomer")]
        public async Task<IActionResult> CreateOrderByCustomer([FromBody] OrderView newOrder)
        {
            bool checkOrder = await _orderManagementService.CreateOrderByCustomer(newOrder);
            if (checkOrder)
            {
                return Ok("Create order success");
            }
            else
            {
                return BadRequest("Create order fail");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/orders/createOrderDetail")]
        public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetailView newOrder)
        {
            bool checkOrder = await _orderManagementService.CreateOrderDetail(newOrder);
            if (checkOrder)
            {
                return Ok("Create order detail success");
            }
            else
            {
                return BadRequest("Create order detail fail");
            }
        }

        [Authorize(Roles = "Staff, Customer")]
        [HttpPut("/api/v1/orders/updateStatusOrderByOrderId/{id}")]
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

        [Authorize(Roles = "Staff")]
        [HttpPut("/api/v1/orders/changeOrderDeliveryFailedFee/{id}")]
        public async Task<IActionResult> ChangeOrderDeliveryFailedFee(int id, [FromBody] decimal newDeliveryFailedFee)
        {
            try
            {
                var success = await _orderManagementService.ChangeOrderDeliveryFailedFee(id, newDeliveryFailedFee);

                if (success)
                {
                    return Ok("Order Delivery Failed Fee updated successfully");
                }
                else
                {
                    return NotFound("Order not found or failed to update Delivery Failed Fee");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
