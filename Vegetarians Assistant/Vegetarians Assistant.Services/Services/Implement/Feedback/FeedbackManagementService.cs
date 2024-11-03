﻿using AutoMapper;
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
                    if (feedback.DishId > 0)
                    {
                        dishIds.Add(feedback.DishId);
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
                    if (feedback.UserId > 0)
                    {
                        userIds.Add(feedback.UserId);
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
                        DishName = preferenceDictionary1.ContainsKey(feedback.DishId) ? preferenceDictionary1[feedback.DishId] : null,
                        UserId = feedback.UserId,
                        Username = preferenceDictionary2.ContainsKey(feedback.UserId) ? preferenceDictionary2[feedback.UserId] : null,
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

        public async Task<List<FeedbackView?>> GetFeedbackByDishId(int id)
        {
            try
            {
                var feedbacks = await _unitOfWork.FeedbackRepository.GetAsync(c => c.DishId == id);
                List<FeedbackView> feedbackViews = new List<FeedbackView>();

                var dishIds = new HashSet<int>();
                foreach (var feedback in feedbacks)
                {
                    if (feedback.DishId > 0)
                    {
                        dishIds.Add(feedback.DishId);
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
                    if (feedback.UserId > 0)
                    {
                        userIds.Add(feedback.UserId);
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
                        FeedbackId = feedback.FeedbackId,
                        DishId = feedback.DishId,
                        DishName = preferenceDictionary2.ContainsKey(feedback.UserId) ? preferenceDictionary2[feedback.UserId] : null,
                        UserId = feedback.UserId,
                        Username = preferenceDictionary2.ContainsKey(feedback.UserId) ? preferenceDictionary2[feedback.UserId] : null,
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

    }
}
