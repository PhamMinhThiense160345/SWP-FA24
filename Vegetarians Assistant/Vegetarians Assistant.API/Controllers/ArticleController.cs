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
    }
}
