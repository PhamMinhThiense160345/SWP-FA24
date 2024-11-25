using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IArticleImage;

namespace Vegetarians_Assistant.API.Controllers
{
    public class ArticleImageController : ControllerBase
    {
        private readonly IArticleImageManagementService _articleImageManagementService;
        public ArticleImageController(IArticleImageManagementService articleImageManagementService)
        {
            _articleImageManagementService = articleImageManagementService;
        }

        [Authorize(Roles = "Customer,Moderator,Nutritionist")]
        [HttpGet("/api/v1/articleImages/getArticleImageByArticleId/{id}")]
        public async Task<ActionResult<IEnumerable<ArticleImageView>>> GetArticleImageByArticleId(int id)
        {
            var articleImagesList = await _articleImageManagementService.GetArticleImageByArticleId(id);
            if (articleImagesList == null)
            {
                return NotFound("Article Images not found");
            }
            return Ok(articleImagesList);
        }

        [Authorize(Roles = "Customer,Nutritionist")]
        [HttpPost("/api/v1/articleImages/createArticleImage")]
        public async Task<IActionResult> CreateArticleImage([FromBody] ArticleImageView newArticleImage)
        {
            bool checkArticleImage = await _articleImageManagementService.CreateArticleImage(newArticleImage);
            if (checkArticleImage)
            {
                return Ok("Create article image success");
            }
            else
            {
                return BadRequest("Create article image fail");
            }
        }

        [Authorize(Roles = "Nutritionist")]
        [HttpPut("/api/v1/articleImages/updateArticleImageByArticleImageId/{id}")]
        public async Task<IActionResult> UpdateArticleImageByArticleImageId(int id, [FromQuery] string newImage)
        {
            try
            {
                var success = await _articleImageManagementService.UpdateArticleImageByArticleImageId(id, newImage);

                if (success)
                {
                    return Ok("Article image detail updated successfully");
                }
                else
                {
                    return NotFound("Article image not found or failed to update article image detail");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Nutritionist")]
        [HttpDelete("/api/v1/articleImages/deleteArticleImageByArticleImageId/{id}")]
        public async Task<IActionResult> DeleteArticleImageByArticleImageId(int id)
        {
            var success = await _articleImageManagementService.DeleteArticleImageByArticleImageId(id);

            if (success)
            {
                return Ok("Delete article image successfully");
            }
            else
            {
                return NotFound("Article image not found or failed to delete");
            }
        }

    }
}
