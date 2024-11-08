using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.API.Helpers;
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
        public ArticleController(IArticleService articleService, ICommentHelper commentHelper)
        {
            _articleService = articleService;
            _commentHelper = commentHelper;
        }

        [HttpGet("/api/v1/articles/allArticleByRoleId/{id}")]
        public async Task<ActionResult<IEnumerable<ArticleView>>> GetArticleByRoleId(int id)

        {
            var articleDetail = await _articleService.GetArticleByRoleId(id);
            if (articleDetail == null)
            {
                return NotFound("Article not found");
            }
            return Ok(articleDetail);
        }

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

        [HttpPost("/api/v1/articles/createArticleByCustomer")]
        public async Task<IActionResult> createArticleByCustomer([FromBody] ArticleView newArticle)
        {
            bool checkRegister = await _articleService.CreateArticleByCustomer(newArticle);
            if (checkRegister)
            {
                return Ok("Create success");
            }
            else
            {
                return BadRequest("Not correct role");
            }
        }

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

        [HttpPost("changeStatus/{id}")]
        public async Task<IActionResult> changeStatusArticle(int id)
        {
            try
            {
                var article = await _articleService.changeStatus(id);
                if (article == null)
                {
                    return BadRequest("Cập nhập trạng thái bài viết thất bại");
                }

                return Ok(article);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
        }
    }
        
    
