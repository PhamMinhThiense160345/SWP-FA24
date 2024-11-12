using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IArticleLike;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleLikeController : ControllerBase
    {
        private readonly IArticleLikeManagementService _articleLikeManagementService;
        public ArticleLikeController(IArticleLikeManagementService articleLikeManagementService)
        {
            _articleLikeManagementService = articleLikeManagementService;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("/api/v1/articles/getArticleLikeByArticleId/{id}")]
        public async Task<ActionResult<IEnumerable<ArticleLikeView>>> GetArticleLikeByArticleId(int id)
        {
            var checkArticle = await _articleLikeManagementService.GetArticleLikeByArticleId(id);
            if (checkArticle == null || !checkArticle.Any())
            {
                return NotFound("No one like for article");
            }
            return Ok(checkArticle);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/articles/createArticleLike")]
        public async Task<IActionResult> CreateArticleLike([FromBody] ArticleLikeView newArticleLike)
        {
            bool checkArticle = await _articleLikeManagementService.CreateArticleLike(newArticleLike);
            if (checkArticle)
            {
                return Ok("Like success");
            }
            else
            {
                return BadRequest("UnLike success");
            }
        }
    }
}
