using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Feedback;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManagementService _feedbackManagementService;
        public FeedbackController(IFeedbackManagementService feedbackManagementService)
        {
            _feedbackManagementService = feedbackManagementService;
        }

        [Authorize(Roles = "Customer, Nutritionist")]
        [HttpGet("/api/v1/feedbacks/allfeedback")]
        public async Task<ActionResult<IEnumerable<FeedbackInfoView>>> GetFeedbacks()
        {

            var feedbacksList = await _feedbackManagementService.GetAllFeedback();
            if (feedbacksList == null)
            {
                return NotFound("No feedbacks found on the system");
            }
            return Ok(feedbacksList);
        }

        [Authorize(Roles = "Customer, Nutritionist")]
        [HttpGet("/api/v1/feedbacks/getFeedbackByDishID/{id}")]
        public async Task<ActionResult<FeedbackInfoView>> GetFeedbackByID(int id)
        {
            var feedbackDetail = await _feedbackManagementService.GetFeedbackByDishId(id);
            if (feedbackDetail == null)
            {
                return NotFound("Feedbacks not found");
            }
            return Ok(feedbackDetail);
        }

        [Authorize(Roles = "Customer, Nutritionist")]
        [HttpPost("/api/v1/feedbacks/createFeedback")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackView newFeedback)
        {
            bool checkFeedback = await _feedbackManagementService.CreateFeedback(newFeedback);
            if (checkFeedback)
            {
                return Ok("Create feedback success");
            }
            else
            {
                return BadRequest("Create feedback fail");
            }
        }

    }
}
