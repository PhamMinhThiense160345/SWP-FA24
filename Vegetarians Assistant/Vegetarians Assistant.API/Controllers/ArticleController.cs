using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.API.Helpers;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.ArticleImp;
using Vegetarians_Assistant.Services.Services.Interface.IArticle;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ICommentHelper _commentHelper;
        private readonly IUnitOfWork _unitOfWork;
        public ArticleController(IArticleService articleService, ICommentHelper commentHelper, IUnitOfWork unitOfWork)
        {
            _articleService = articleService;
            _commentHelper = commentHelper;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Customer,Moderator,Nutritionist")]
        [HttpGet("/api/v1/articles/allArticleByRoleId/{id}")]
        public async Task<ActionResult<IEnumerable<ArticleView>>> GetArticleByRoleId(int id)

        {
            var articleDetail = await _articleService.GetArticleByRoleId(id);
            if (articleDetail == null || !articleDetail.Any())
            {
                return NotFound("Article not found");
            }
            return Ok(articleDetail);
        }

        [Authorize(Roles = "Customer,Moderator,Nutritionist")]
        [HttpGet("{id}")]
        public async Task<IActionResult> getArticleDetail(int id)
        {
            try
            {
                var article = await _articleService.GetById(id);
                return Ok(article);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("comment/{id}")]
        public async Task<IActionResult> getArticleComment(int id)
        {
            try
            {
                var comments = await _articleService.getArticleComment(id);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer,Moderator")]
        [HttpGet("/api/v1/articles/getArticleByAuthorId/{id}")]
        public async Task<ActionResult<IEnumerable<ArticleView>>> GetArticleByAuthorId(int id)
        {
            var articleDetail = await _articleService.GetArticleByAuthorId(id);
            if (articleDetail == null)
            {
                return NotFound("Article not found");
            }
            return Ok(articleDetail);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/articles/createArticleByCustomer")]
        public async Task<IActionResult> createArticleByCustomer([FromBody] ArticleView newArticle)
        {
            bool checkArticle = await _articleService.CreateArticleByCustomer(newArticle);
            if (checkArticle)
            {
                return Ok("Create success");
            }
            else
            {
                return BadRequest("Not correct role");
            }
        }

        [Authorize(Roles = "Nutritionist")]
        [HttpPost("editArticle")]
        public async Task<IActionResult> editArticle([FromBody] ArticleView view)
        {
            try
            {
                var article = await _articleService.Edit(view);
                if (article == null)
                {
                    return BadRequest("Cập nhập bài viết thất bại");
                }

                return Ok(article);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("comment")]
        public async Task<IActionResult> postComment(CommentView view)
        {
            try
            {
                await _articleService.postComment(view);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Nutritionist")]
        [HttpPost("/api/v1/articles/createArticleByNutritionist")]
        public async Task<IActionResult> createArticleByNutritionist([FromBody] ArticleView newArticle)
        {
            bool checkRegister = await _articleService.CreateArticleByNutritionist(newArticle);
            if (checkRegister)
            {
                return Ok("Create success");
            }
            else
            {
                return BadRequest("Not correct role");
            }
        }

        [HttpPost("checkCommentContent")]
        public IActionResult CheckCommentContent([FromBody] CheckCommentContentView request)
       => Ok(_commentHelper.CheckContent(request.Content));

        [Authorize(Roles = "Nutritionist")]
        [HttpPut("/api/v1/articles/updateArticleStatusByArticleId/{id}")]
        public async Task<IActionResult> UpdateArticleStatusByArticleId(int id, [FromBody] string newStatus)
        {
            try
            {
                var success = await _articleService.UpdateArticleStatusByArticleId(id, newStatus);

                if (success)
                {
                    return Ok("Article status updated successfully");
                }
                else
                {
                    return NotFound("Article status not found or failed to update status");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("/api/v1/articles/deleteCommentByUserId")]
        public async Task<IActionResult> DeleteCommentByUserId([FromBody] CommentView deleteComment)
        {
            var isCommetExist = (await _unitOfWork.CommentRepository.FindAsync(c => c.ArticleId == deleteComment.ArticleId && c.UserId == deleteComment.UserId)).FirstOrDefault();
            if (isCommetExist != null)
            {
                bool checkComment = await _articleService.DeleteCommentByUserId(deleteComment);
                if (checkComment)
                {
                    return Ok("Delete comment success");
                }
                else
                {
                    return BadRequest("Delete comment fail");
                }
            }
            else
            {
                return BadRequest("This comment no exist.");
            }
        }

    }
}
        
    
