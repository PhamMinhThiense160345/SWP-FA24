using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Favorite;

namespace Vegetarians_Assistant.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteManagementService _favoriteManagementService;
        private readonly IUnitOfWork _unitOfWork;
        public FavoriteController(IFavoriteManagementService favoriteManagementService, IUnitOfWork unitOfWork)
        {
            _favoriteManagementService = favoriteManagementService;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("/api/v1/favorites/allDishFavoriteByUserId/{id}")]
        public async Task<ActionResult<FavoriteDishView>> GetAllDishFavoriteByUserId(int id)
        {
            var favoriteDetail = await _favoriteManagementService.GetAllDishFavoriteByUserId(id);
            if (favoriteDetail == null)
            {
                return NotFound("Favorites not found");
            }
            return Ok(favoriteDetail);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/favorites/createFavoriteDish")]
        public async Task<IActionResult> CreateFavoriteDish([FromBody] FavoriteView newFavorite)
        {
            var isFavoriteExist = (await _unitOfWork.FavoriteDishRepository.FindAsync(c => c.DishId == newFavorite.DishId && c.UserId == newFavorite.UserId)).FirstOrDefault();
            if (isFavoriteExist == null)
            {
                bool checkFavorite = await _favoriteManagementService.CreateFavoriteDish(newFavorite);
                if (checkFavorite)
                {
                    return Ok("Add Favorite success");
                }
                else
                {
                    return BadRequest("Add Favorite fail");
                }
            }
            else
            {
                return BadRequest("Dish had favorite");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("/api/v1/favorites/deleteFavoriteDish")]
        public async Task<IActionResult> DeleteFavoriteDish([FromBody] FavoriteView newFavorite)
        {
            var isFavoriteExist = (await _unitOfWork.FavoriteDishRepository.FindAsync(c => c.DishId == newFavorite.DishId && c.UserId == newFavorite.UserId)).FirstOrDefault();
            if (isFavoriteExist != null)
            {
                bool checkFavorite = await _favoriteManagementService.DeleteFavoriteDish(newFavorite);
                if (checkFavorite)
                {
                    return Ok("Delete Favorite success");
                }
                else
                {
                    return BadRequest("Delete Favorite fail");
                }
            }
            else
            {
                return BadRequest("The dish has not been added to your favorites list.");
            }
        }

    }
}
