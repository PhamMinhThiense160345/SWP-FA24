using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.INutritionCriterion;

namespace Vegetarians_Assistant.API.Controllers
{
    public class NutritionCriterionController : ControllerBase
    {
        private readonly INutritionCriterionManagementService _nutritionCriterionManagementService;
        public NutritionCriterionController(INutritionCriterionManagementService nutritionCriterionManagementService)
        {
            _nutritionCriterionManagementService = nutritionCriterionManagementService;
        }
        [HttpGet("/api/v1/nutritionCriterions/getAllNutritionCriteria")]
        public async Task<ActionResult<IEnumerable<NutritionCriterionView>>> GetAllNutritionCriteria()
        {

            var nutritionCriterionsList = await _nutritionCriterionManagementService.GetAllNutritionCriteria();
            if (nutritionCriterionsList == null || !nutritionCriterionsList.Any())
            {
                return NotFound("No nutrition criterions found on the system");
            }
            return Ok(nutritionCriterionsList);
        }
        [HttpGet("/api/v1/nutritionCriterions/getNutritionCriteriaDetailByCriteriaId/{id}")]
        public async Task<ActionResult<NutritionCriterionView>> GetNutritionCriteriaDetailByCriteriaId(int id)
        {
            var nutritionCriterionsList = await _nutritionCriterionManagementService.GetNutritionCriteriaDetailByCriteriaId(id);
            if (nutritionCriterionsList == null)
            {
                return NotFound("Nutrition Criteria detail not found");
            }
            return Ok(nutritionCriterionsList);
        }
        [HttpPost("/api/v1/nutritionCriterions/createNutritionCriteria")]
        public async Task<IActionResult> CreateNutritionCriteria([FromBody] NutritionCriterionView newNutritionCriteria)
        {
            bool checkNutritionCriteria = await _nutritionCriterionManagementService.CreateNutritionCriteria(newNutritionCriteria);
            if (checkNutritionCriteria)
            {
                return Ok("Create nutrition criteria success");
            }
            else
            {
                return BadRequest("Create nutrition criteria fail");
            }
        }
    }
}
