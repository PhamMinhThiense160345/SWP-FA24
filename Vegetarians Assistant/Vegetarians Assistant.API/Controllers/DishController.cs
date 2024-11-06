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

        [HttpGet("/api/v1/dishs/allDish")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetDishs()
        {

            var dishsList = await _dishManagementService.GetAllDish();//test
            if (dishsList == null || !dishsList.Any())
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

        [HttpGet("dishs/calculateNutrition/{dishId}")]
        public async Task<IActionResult> CalculateNutrition(int dishId)
        {
            var response = await _dishManagementService.CalculateNutrition(dishId);
            if (response is null)
            {
                return NotFound("Not found dish with id = " + dishId);
            }
            return Ok(response);

        }

        [HttpPost("addIngredient")]
        public async Task<IActionResult> AddIngredientToDish([FromBody] AddIngredientView request)
        {
            var response = await _dishManagementService.AddIngredientAsync(request);
            return Ok(response);
        }

        [HttpPut("/api/v1/dishs/updateDishDetailByDishId")]
        public async Task<IActionResult> UpdateDishDetailByDishId([FromBody] DishView updateDish)
        {
            try
            {
                var success = await _dishManagementService.UpdateDishDetailByDishId(updateDish);

                if (success)
                {
                    return Ok("Dish detail updated successfully");
                }
                else
                {
                    return NotFound("Dish not found or failed to update dish detail");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
