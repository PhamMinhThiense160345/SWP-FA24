using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface;
using Vegetarians_Assistant.Services.Services.Interface.IShipping;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingManagementService _shippingManagementService;

        public ShippingController(IShippingManagementService shippingManagementService)
        {
            _shippingManagementService = shippingManagementService;
        }


        // [Authorize(Roles = "Admin,Customer")]
        [HttpPost("createShipping")]
        public async Task<IActionResult> CreateShipping([FromBody] ShippingView newShipping)
        {
            var result = await _shippingManagementService.CreateShipping(newShipping);
            if (result)
            {
                return Ok("Create shipping success");
            }
            else
            {
                return BadRequest("Create shipping fail");
            }
        }


        // [Authorize(Roles = "Customer,Admin")]
        [HttpGet("getShippingByOrderId/{orderId}")]
        public async Task<ActionResult<ShippingView>> GetShippingByOrderId(int orderId)
        {
            var shipping = await _shippingManagementService.GetShippingByOrderId(orderId);
            if (shipping == null)
            {
                return NotFound("Shipping not found");
            }
            return Ok(shipping);
        }

     
       
       // [Authorize(Roles = "Admin")]
        [HttpPut("updateShipping/{orderId}")]
        public async Task<IActionResult> UpdateShippingByOrderId(int orderId, [FromBody] ShippingView updatedShipping)
        {
            var result = await _shippingManagementService.UpdateShippingByOrderId(orderId, updatedShipping);
            if (result)
            {
                return Ok("Update shipping success");
            }
            else
            {
                return NotFound("Shipping not found or update failed");
            }
        }


    }
}
