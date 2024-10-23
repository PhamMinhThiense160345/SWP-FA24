using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
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

        [HttpPost]
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


    }
}
