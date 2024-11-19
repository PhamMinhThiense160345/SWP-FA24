using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IArticleLike;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleLikeController : ControllerBase
    {
        private readonly IArticleLikeManagementService _articleLikeManagementService;
        private readonly IUnitOfWork _unitOfWork;
        public ArticleLikeController(IArticleLikeManagementService articleLikeManagementService, IUnitOfWork unitOfWork)
        {
            _articleLikeManagementService = articleLikeManagementService;
            _unitOfWork = unitOfWork;
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

        [Authorize(Roles = "Customer")]
        [HttpDelete("/api/v1/articles/deleteArticleLikeByUserId")]
        public async Task<IActionResult> DeleteArticleLikeByUserId([FromBody] ArticleLikeView deleteArticleLike)
        {
            var isLikeExist = (await _unitOfWork.ArticleLikeRepository.FindAsync(c => c.ArticleId == deleteArticleLike.ArticleId && c.UserId == deleteArticleLike.UserId)).FirstOrDefault();
            if (isLikeExist != null)
            {
                bool checkComment = await _articleLikeManagementService.DeleteArticleLikeByUserId(deleteArticleLike);
                if (checkComment)
                {
                    return Ok("Delete like success");
                }
                else
                {
                    return BadRequest("Delete like fail");
                }
            }
            else
            {
                return BadRequest("This like no exist.");
            }
        }

    }
}
