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


    }
}
