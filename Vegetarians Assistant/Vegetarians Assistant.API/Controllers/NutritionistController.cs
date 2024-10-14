using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Nutritionist;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionistController : ControllerBase
    {
        private readonly IDishManagementService _dishManagementService;
        public NutritionistController(IDishManagementService dishManagementService)
        {
            _dishManagementService = dishManagementService;
        }
        [HttpGet("/api/v1/nutritionists/alldish")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetDishs()
        {

            var dishsList = await _dishManagementService.GetAllDish();
            if (dishsList.IsNullOrEmpty())
            {
                return NotFound("No dishs found on the system");
            }
            return Ok(dishsList);
        }

        [HttpGet("/api/v1/nutritionists/getDishByName/{name}")]
        public async Task<ActionResult<DishView>> GetDishByNameDish(string name)
        {
            var dishDetail = await _dishManagementService.GetDishByname(name);
            if (dishDetail == null)
            {
                return NotFound("Dish not found");
            }
            return dishDetail;
        }
    }
}
