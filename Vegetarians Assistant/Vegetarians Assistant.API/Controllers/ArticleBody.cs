using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IArticleBody;

namespace Vegetarians_Assistant.API.Controllers
{
    public class ArticleBodyController : ControllerBase
    {
        private readonly IArticleBodyManagementService _articleBodyManagementService;

        public ArticleBodyController(IArticleBodyManagementService articleBodyManagementService)
        {
            _articleBodyManagementService = articleBodyManagementService;
        }

        //[Authorize(Roles = "Customer,Moderator,Nutritionist")]
        [HttpGet("/api/v1/articleBodies/getArticleBodyByArticleId/{id}")]
        public async Task<ActionResult<IEnumerable<ArticleBodyView>>> GetArticleBodyByArticleId(int id)
        {
            var articleBodyList = await _articleBodyManagementService.GetArticleBodyByArticleId(id);
            if (articleBodyList == null || !articleBodyList.Any())
            {
                return NotFound("Article bodies not found");
            }
            return Ok(articleBodyList);
        }

        //[Authorize(Roles = "Customer,Nutritionist")]
        [HttpPost("/api/v1/articleBodies/createArticleBody")]
        public async Task<IActionResult> CreateArticleBody([FromBody] ArticleBodyView newArticleBody)
        {
            var result = await _articleBodyManagementService.CreateArticleBody(newArticleBody);
            if (result)
            {
                return Ok("Create article body success");
            }
            else
            {
                return BadRequest("Create article body fail");
            }
        }

       // [Authorize(Roles = "Nutritionist")]
        [HttpPut("/api/v1/articleBodies/updateArticleBodyByBodyId/{id}")]
        public async Task<IActionResult> UpdateArticleBodyByBodyId(int id, [FromBody] ArticleBodyView updatedArticleBody)
        {
            var result = await _articleBodyManagementService.UpdateArticleBodyByBodyId(id, updatedArticleBody);
            if (result)
            {
                return Ok("Update article body success");
            }
            else
            {
                return NotFound("Article body not found or update failed");
            }
        }

        //[Authorize(Roles = "Nutritionist")]
        [HttpDelete("/api/v1/articleBodies/deleteArticleBodyByBodyId/{id}")]
        public async Task<IActionResult> DeleteArticleBodyByBodyId(int id)
        {
            var result = await _articleBodyManagementService.DeleteArticleBodyByBodyId(id);
            if (result)
            {
                return Ok("Delete article body success");
            }
            else
            {
                return NotFound("Article body not found or delete failed");
            }
        }
    }
}
