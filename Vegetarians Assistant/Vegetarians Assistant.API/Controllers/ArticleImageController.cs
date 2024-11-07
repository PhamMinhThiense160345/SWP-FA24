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

    }
}
