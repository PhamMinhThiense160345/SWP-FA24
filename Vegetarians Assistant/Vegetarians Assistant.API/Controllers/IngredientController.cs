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

        [Authorize(Roles = "Customer,Nutritionist")]
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

        [Authorize(Roles = "Nutritionist")]
        [HttpPost("/api/v1/ingredients/createIngredient")]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientInfoView newIngredient)
        {
            bool checkIngredient = await _ingredientManagementService.CreateIngredient(newIngredient);
            if (checkIngredient)
            {
                return Ok("Create ingredient success");
            }
            else
            {
                return BadRequest("Ingredient already exists");
            }
        }

        [Authorize(Roles = "Nutritionist")]
        [HttpPut("/api/v1/ingredients/updateIngredient")]
        public async Task<IActionResult> UpdateIngredient([FromBody] IngredientInfoView updateIngredient)
        {
            try
            {
                var success = await _ingredientManagementService.UpdateIngredient(updateIngredient);

                if (success)
                {
                    return Ok("Ingredient updated successfully");
                }
                else
                {
                    return NotFound("Ingredient not found or failed to update ingredient detail");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Nutritionist")]
        [HttpDelete("/api/v1/ingredients/deleteIngredient/{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var success = await _ingredientManagementService.DeleteIngredient(id);

            if (success)
            {
                return Ok("Delete ingredient successfully");
            }
            else
            {
                return NotFound("Ingredient not found or failed to delete");
            }
        }

    }
}
