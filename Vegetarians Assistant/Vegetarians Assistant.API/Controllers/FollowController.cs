using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IFollowImp;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowManagementService _followManagementService;
        private readonly IUnitOfWork _unitOfWork;
        public FollowController(IFollowManagementService followManagementService, IUnitOfWork unitOfWork)
        {
            _followManagementService = followManagementService;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("/api/v1/follows/allFollowerByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetAllFollowerByUserId(int id)
        {
            var followerDetail = await _followManagementService.GetAllFollowerByUserId(id);
            if (followerDetail == null)
            {
                return NotFound("Follower not found");
            }
            return Ok(followerDetail);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("/api/v1/follows/allFollowingByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<DishView>>> GetAllFollowingByUserId(int id)
        {
            var followingDetail = await _followManagementService.GetAllFollowingByUserId(id);
            if (followingDetail == null)
            {
                return NotFound("Following not found");
            }
            return Ok(followingDetail);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/follows/followerUserByCustomer")]
        public async Task<IActionResult> FollowerUserByCustomer([FromBody] FollowerView newFollow)
        {
            var isFollowExist = (await _unitOfWork.FollowerRepository.FindAsync(c => c.FollowerUserId == newFollow.FollowerUserId)).FirstOrDefault();
            if (isFollowExist == null)
            {
                bool checkFollow = await _followManagementService.FollowerUserByCustomer(newFollow);
                if (checkFollow)
                {
                    return Ok("Follow success");
                }
                else
                {
                    return BadRequest("Follow fail");
                }
            }
            else
            {
                bool checkFollow = await _followManagementService.FollowerUserByCustomer(newFollow);
                if (checkFollow)
                {
                    return Ok("Unfollow success");
                }
                else
                {
                    return BadRequest("Unfollow fail");
                }
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("/api/v1/follows/followingUserByCustomer")]
        public async Task<IActionResult> FollowingUserByCustomer([FromBody] FollowingView newFollow)
        {
            var isFollowExist = (await _unitOfWork.FollowingRepository.FindAsync(c => c.FollowingUserId == newFollow.FollowingUserId)).FirstOrDefault();
            if (isFollowExist == null)
            {
                bool checkFollow = await _followManagementService.FollowingUserByCustomer(newFollow);
                if (checkFollow)
                {
                    return Ok("Follow success");
                }
                else
                {
                    return BadRequest("Follow fail");
                }
            }
            else
            {
                bool checkFollow = await _followManagementService.FollowingUserByCustomer(newFollow);
                if (checkFollow)
                {
                    return Ok("Unfollow success");
                }
                else
                {
                    return BadRequest("Unfollow fail");
                }
            }
        }

    }
}
