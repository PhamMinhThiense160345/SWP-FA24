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
        [HttpGet("/api/v1/favorites/allDishByMenuId/{id}")]
        public async Task<ActionResult<MenuDishView>> GetAllDishByMenuId(int id)
        {
            var favoriteDetail = await _favoriteManagementService.GetAllDishByMenuId(id);
            if (favoriteDetail == null)
            {
                return NotFound("Favorites not found");
            }
            return Ok(favoriteDetail);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("/api/v1/favorites/allMenuByUserId/{id}")]
        public async Task<ActionResult<MenuInfoView>> GetAllMenuByUserId(int id)
        {
            var favoriteDetail = await _favoriteManagementService.GetAllMenuByUserId(id);
            if (favoriteDetail == null)
            {
                return NotFound("Favorites not found");
            }
            return Ok(favoriteDetail);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("/api/v1/favorites/getLatestMenuByUserId/{id}")]
        public async Task<IActionResult> GetLatestMenuByUserId(int id)
        {
            try
            {
                var latestMenu = await _favoriteManagementService.GetLatestMenuByUserId(id);
                if (latestMenu != null)
                {
                    return Ok(latestMenu);
                }
                return NotFound("No menu found for this user.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/favorites/createFavoriteMenu")]
        public async Task<IActionResult> CreateFavoriteMenu([FromBody] MenuView newFavorite)
        {
                bool checkFavorite = await _favoriteManagementService.CreateFavoriteMenu(newFavorite);
                if (checkFavorite)
                {
                    return Ok("Add Favorite menu success");
                }
                else
                {
                    return BadRequest("Add Favorite menu fail");
                }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/favorites/createDishForFavoriteMenu")]
        public async Task<IActionResult> CreateDishForFavoriteMenu([FromBody] MenuDishView newDish)
        {
            bool checkFavorite = await _favoriteManagementService.CreateDishForFavoriteMenu(newDish);
            if (checkFavorite)
            {
                return Ok("Add dish success");
            }
            else
            {
                return BadRequest("Add dish fail");
            }
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

        [Authorize(Roles = "Customer")]
        [HttpDelete("/api/v1/favorites/deleteAllDishByMenuId/{id}")]
        public async Task<IActionResult> DeleteAllDishByMenuId(int id)
        {
            var success = await _favoriteManagementService.DeleteAllDishByMenuId(id);

            if (success)
            {
                return Ok("Delete dish favorite menu successfully");
            }
            else
            {
                return NotFound("Dish favorite menu not found or failed to delete");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("/api/v1/favorites/deleteMenuByMenuId/{id}")]
        public async Task<IActionResult> DeleteMenuByMenuId(int id)
        {
            var success = await _favoriteManagementService.DeleteMenuByMenuId(id);

            if (success)
            {
                return Ok("Delete menu favorite successfully");
            }
            else
            {
                return NotFound("Menu favorite not found or failed to delete");
            }
        }

    }
}
