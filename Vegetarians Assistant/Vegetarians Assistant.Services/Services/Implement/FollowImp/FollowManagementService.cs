using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
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

        public async Task<bool> FollowingUserByCustomer(FollowingView newFollow)
        {
            try
            {
                bool status = false;

                var follow = _mapper.Map<Following>(newFollow);
                var isFollowExist = (await _unitOfWork.FollowingRepository.FindAsync(c => c.FollowingUserId == newFollow.FollowingUserId)).ToList();
                if (isFollowExist.Any())
                {
                    foreach (var isFollowExists in isFollowExist)
                    {
                        await _unitOfWork.FollowingRepository.DeleteAsync(isFollowExists);
                    }
                    await _unitOfWork.SaveAsync();
                }
                else
                {
                    await _unitOfWork.FollowingRepository.InsertAsync(follow);
                    await _unitOfWork.SaveAsync();
                }
                var insertedArticle = await _unitOfWork.FollowingRepository.GetByIDAsync(follow.FollowingUserId);

                if (insertedArticle != null)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                var insertedArticle = (await _unitOfWork.FollowingRepository.FindAsync(a => a.FollowingId == newFollow.FollowingId)).FirstOrDefault();
                if (insertedArticle != null)
                {
                    await _unitOfWork.FollowingRepository.DeleteAsync(insertedArticle);
                    await _unitOfWork.SaveAsync();
                }

                throw new Exception($"Lỗi khi follow: {ex.Message}");
            }
        }

        public async Task<bool> FollowerUserByCustomer(FollowerView newFollow)
        {
            try
            {
                bool status = false;
                var follow = _mapper.Map<Follower>(newFollow);
                var isFollowExist = (await _unitOfWork.FollowerRepository.FindAsync(c => c.FollowerUserId == newFollow.FollowerUserId)).ToList();
                if (isFollowExist.Any())
                {
                    foreach (var isFollowExists in isFollowExist)
                    {
                        await _unitOfWork.FollowerRepository.DeleteAsync(isFollowExists);
                    }
                    await _unitOfWork.SaveAsync();
                }
                else
                {
                    await _unitOfWork.FollowerRepository.InsertAsync(follow);
                    await _unitOfWork.SaveAsync();
                }
                

                var insertedArticle = await _unitOfWork.FollowerRepository.GetByIDAsync(follow.FollowerUserId);

                if (insertedArticle != null)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                var insertedArticle = (await _unitOfWork.FollowerRepository.FindAsync(a => a.FollowerId == newFollow.FollowerId)).FirstOrDefault();
                if (insertedArticle != null)
                {
                    await _unitOfWork.FollowerRepository.DeleteAsync(insertedArticle);
                    await _unitOfWork.SaveAsync();
                }

                throw new Exception($"Lỗi khi follow: {ex.Message}");
            }
        }

    }
}
