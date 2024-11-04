using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Favorite;

namespace Vegetarians_Assistant.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteManagementService _favoriteManagementService;
        public FavoriteController(IFavoriteManagementService favoriteManagementService)
        {
            _favoriteManagementService = favoriteManagementService;
        }

        [HttpGet("/api/v1/favorites/getAllDishFavoriteByUserId/{id}")]
        public async Task<ActionResult<FavoriteDishView>> GetAllDishFavoriteByUserId(int id)
        {
            var favoriteDetail = await _favoriteManagementService.GetAllDishFavoriteByUserId(id);
            if (favoriteDetail == null)
            {
                return NotFound("Favorites not found");
            }
            return Ok(favoriteDetail);
        }
    }
}
