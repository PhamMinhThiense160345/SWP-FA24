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

        [HttpGet("/api/v1/feedbacks/allfeedback")]
        public async Task<ActionResult<IEnumerable<FeedbackView>>> GetFeedbacks()
        {

            var feedbacksList = await _feedbackManagementService.GetAllFeedback();
            if (feedbacksList.IsNullOrEmpty())
            {
                return NotFound("No feedbacks found on the system");
            }
            return Ok(feedbacksList);
        }

        [HttpGet("/api/v1/feedbacks/getFeedbackByDishID/{id}")]
        public async Task<ActionResult<FeedbackView>> GetFeedbackByID(int id)
        {
            var feedbackDetail = await _feedbackManagementService.GetFeedbackByDishId(id);
            if (feedbackDetail == null)
            {
                return NotFound("Feedbacks not found");
            }
            return Ok(feedbackDetail);
        }

    }
}
