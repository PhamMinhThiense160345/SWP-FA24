using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IIngredient;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientManagementService _ingredientManagementService;
        public IngredientController(IIngredientManagementService ingredientManagementService)
        {
            _ingredientManagementService = ingredientManagementService;
        }

        [Authorize(Roles = "Customer,Nutritionist")]
        [HttpGet("/api/v1/ingredients/allIngredient")]
        public async Task<ActionResult<IEnumerable<IngredientInfoView>>> GetAllIngredient()
        {

            var ingredientsList = await _ingredientManagementService.GetAllIngredient();
            if (ingredientsList == null || !ingredientsList.Any())
            {
                return NotFound("No ingredient found on the system");
            }
            return Ok(ingredientsList);
        }

        [Authorize(Roles = "Customer,Nutritionist")]
        [HttpGet("/api/v1/ingredients/getIngredientByIngredientId/{id}")]
        public async Task<ActionResult<IngredientInfoView>> GetIngredientByIngredientId(int id)
        {
            var ingredientDetail = await _ingredientManagementService.GetIngredientByIngredientId(id);
            if (ingredientDetail == null)
            {
                return NotFound("Ingredient not found");
            }
            return Ok(ingredientDetail);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("/api/v1/ingredients/getIngredientByDishId/{id}")]
        public async Task<ActionResult<IEnumerable<DishIngredientView>>> GetIngredientByDishId(int id)
        {
            var checkIngredient = await _ingredientManagementService.GetIngredientByDishId(id);
            if (checkIngredient == null || !checkIngredient.Any())
            {
                return NotFound("No ingredient by dish");
            }
            return Ok(checkIngredient);
        }

    }
}
