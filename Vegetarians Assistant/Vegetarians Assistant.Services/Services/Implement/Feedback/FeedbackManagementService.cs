using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Feedback;

namespace Vegetarians_Assistant.Services.Services.Implement.Feedback
{
    public class FeedbackManagementService : IFeedbackManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FeedbackManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<FeedbackView>> GetAllFeedback()
        {
            try
            {
                var feedbacks = (await _unitOfWork.FeedbackRepository.GetAsync()).ToList();
                List<FeedbackView> feedbackViews = new List<FeedbackView>();

                var dishIds = new HashSet<int>();
                foreach (var feedback in feedbacks)
                {
                    if (feedback.DishId.HasValue)
                    {
                        dishIds.Add(feedback.DishId.Value);
                    }
                }

                var dishes = await _unitOfWork.DishRepository.GetAsync(dp => dishIds.Contains(dp.DishId));

                var preferenceDictionary1 = new Dictionary<int, string>();
                foreach (var preference in dishes)
                {
                    preferenceDictionary1[preference.DishId] = preference.Name;
                }

                var userIds = new HashSet<int>();
                foreach (var feedback in feedbacks)
                {
                    if (feedback.UserId.HasValue)
                    {
                        userIds.Add(feedback.UserId.Value);
                    }
                }

                var users = await _unitOfWork.UserRepository.GetAsync(dp => userIds.Contains(dp.UserId));

                var preferenceDictionary2 = new Dictionary<int, string>();
                foreach (var preference in users)
                {
                    preferenceDictionary2[preference.UserId] = preference.Username;
                }

                foreach (var feedback in feedbacks)
                {
                    var feedbackView = new FeedbackView()
                    {
                        FeedbackId  =  feedback.FeedbackId,
                        DishId = feedback.DishId,
                        DishName = feedback.DishId.HasValue && preferenceDictionary1.ContainsKey(feedback.DishId.Value)
                    ? preferenceDictionary1[feedback.DishId.Value]
                    : null,
                        UserId = feedback.UserId,
                        Username = feedback.UserId.HasValue && preferenceDictionary2.ContainsKey(feedback.UserId.Value)
                    ? preferenceDictionary2[feedback.UserId.Value]
                    : null,
                        OrderId = feedback.OrderId,
                        Rating = feedback.Rating,
                        FeedbackContent = feedback.FeedbackContent,
                        FeedbackDate = feedback.FeedbackDate
                    };
                    feedbackViews.Add(feedbackView);
                }
                return feedbackViews;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FeedbackView?> GetFeedbackByFeedbackId(int id)
        {
            try
            {
                var feedback = await _unitOfWork.FeedbackRepository.GetByIDAsync(id);
                if (feedback != null)
                {
                    string? dishName = null;
                    if (feedback.DishId.HasValue)
                    {
                        var dish = await _unitOfWork.DishRepository.GetByIDAsync(feedback.DishId.Value);
                        dishName = dish?.Name;
                    }

                    string? userName = null;
                    if (feedback.UserId.HasValue)
                    {
                        var dish = await _unitOfWork.UserRepository.GetByIDAsync(feedback.UserId.Value);
                        userName = dish?.Username;
                    }

                    var feedbackView = new FeedbackView()
                    {
                        FeedbackId = feedback.FeedbackId,
                        DishId = feedback.DishId,
                        DishName = dishName,
                        UserId = feedback.UserId,
                        Username = userName,
                        OrderId = feedback.OrderId,
                        Rating = feedback.Rating,
                        FeedbackContent = feedback.FeedbackContent,
                        FeedbackDate = feedback.FeedbackDate
                    };
                    return feedbackView;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
