using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Dish;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishManagementService _dishManagementService;
        public DishController(IDishManagementService dishManagementService)
        {
            _dishManagementService = dishManagementService;
        }
        [HttpGet("/api/v1/dishs/alldish")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetDishs()
        {

            var dishsList = await _dishManagementService.GetAllDish();
            if (dishsList.IsNullOrEmpty())
            {
                return NotFound("No dishs found on the system");
            }
            return Ok(dishsList);
        }

        [HttpGet("/api/v1/dishs/getDishByName/{name}")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetDishByNameDish(string name)
        {
            var dishDetail = await _dishManagementService.GetDishByname(name);
            if (dishDetail == null)
            {
                return NotFound("Dish not found");
            }
            return Ok(dishDetail);
        }

        [HttpGet("/api/v1/dishs/getDishByDishType/{dishType}")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetDishByDishType(string dishType)
        {
            var dishDetail = await _dishManagementService.GetDishByDishType(dishType);
            if (dishDetail == null)
            {
                return NotFound("Dish not found");
            }
            return Ok(dishDetail);
        }
        [HttpGet("/api/v1/dishs/GetDishByID/{id}")]
        public async Task<ActionResult<DishView>> GetDishByID(int id)
        {
            var dishDetail = await _dishManagementService.GetDishByDishId(id);
            if (dishDetail == null)
            {
                return NotFound("Dishs not found");
            }
            return Ok(dishDetail);
        }
    }
}
