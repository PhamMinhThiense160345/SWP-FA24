using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IFollowImp;

namespace Vegetarians_Assistant.Services.Services.Implement.FollowImp
{
    public class FollowManagementService : IFollowManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FollowManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<FollowerView?>> GetAllFollowerByUserId(int userId)
        {

            try
            {
                var followers = await _unitOfWork.FollowerRepository.FindAsync(c => c.UserId == userId);
                var followerViews = new List<FollowerView>();

                foreach (var follower in followers)
                {
                    followerViews.Add(new FollowerView
                    {
                        FollowerId = follower.FollowerId,
                        UserId = follower.UserId,
                        FollowerUserId = follower.FollowerUserId,
                        FollowDate = follower.FollowDate
                    });
                }
                return followerViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FollowingView?>> GetAllFollowingByUserId(int userId)
        {
            try
            {
                var followings = await _unitOfWork.FollowingRepository.FindAsync(c => c.UserId == userId);
                var followingViews = new List<FollowingView>();

                foreach (var following in followings)
                {
                    followingViews.Add(new FollowingView
                    {
                        FollowingId = following.FollowingId,
                        UserId = following.UserId,
                        FollowingUserId = following.FollowingUserId,
                        FollowDate = following.FollowDate
                    });
                }
                return followingViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
