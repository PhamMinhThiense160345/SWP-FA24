using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IFollowImp;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowManagementService _followManagementService;
        public FollowController(IFollowManagementService followManagementService)
        {
            _followManagementService = followManagementService;
        }
        [HttpGet("/api/v1/follows/getAllFollowerByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetAllFollowerByUserId(int id)
        {
            var followerDetail = await _followManagementService.GetAllFollowerByUserId(id);
            if (followerDetail == null)
            {
                return NotFound("Follower not found");
            }
            return Ok(followerDetail);
        }
        [HttpGet("/api/v1/follows/getAllFollowingByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetAllFollowingByUserId(int id)
        {
            var followingDetail = await _followManagementService.GetAllFollowingByUserId(id);
            if (followingDetail == null)
            {
                return NotFound("Following not found");
            }
            return Ok(followingDetail);
        }
    }
}
