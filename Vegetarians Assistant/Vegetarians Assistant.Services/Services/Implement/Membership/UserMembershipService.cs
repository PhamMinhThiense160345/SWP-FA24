using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.Membership
{
    public class UserMembershipService : IUsermembershipService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserMembershipService( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserMembershipView> addPointForMembership(int userId, int points)
        {
           
            UserMembership membership = await _unitOfWork.UserMembershipRepository.GetByIDAsync(userId);

            if (membership == null)
            {
               
                throw new Exception("Membership not found");
            }
    
            membership.AccumulatedPoints += points;

            var allTiers = await _unitOfWork.MembershipTierRepository.GetAllAsync();
            foreach (var tier in allTiers.OrderBy(t => t.RequiredPoints))
            {
                if (membership.AccumulatedPoints >= tier.RequiredPoints)
                {
                    membership.TierId = tier.TierId;
                }
                else
                {
                    break;
                }
            }
            await _unitOfWork.UserMembershipRepository.UpdateAsync(membership);
            await _unitOfWork.SaveAsync();
            var view = _mapper.Map<UserMembershipView>(membership);
            return view;
        }

        public async Task<UserMembershipView> getCustomerMembership(int userId)
        {
            UserMembership membership = await _unitOfWork.UserMembershipRepository.GetByIDAsync(userId);
            var view = _mapper.Map<UserMembershipView>(membership);
            return view;
        }
        public async Task<ResponseView> insertCustomerMembership(UserMembershipView record)
        {
            var existUser = await _unitOfWork.UserRepository.GetByIDAsync(record.UserId);
            if (existUser is null) return new ResponseView(false, $"User with id = {record.UserId} not exist!");

            var existMembership = await _unitOfWork.UserMembershipRepository.GetByIDAsync(record.UserId);
            if (existMembership is not null) return new ResponseView(false, $"User membership is already exist!");

            var existTier = await _unitOfWork.MembershipTierRepository.GetByIDAsync(record.TierId ?? 0);
            if (existTier is null) return new ResponseView(false, $"Tier with id = {record.TierId} not exist!");

            var membership = new UserMembership()
            {
                UserId = record.UserId,
                TierId = record.TierId,
                AccumulatedPoints = record.AccumulatedPoints,
                DiscountGrantedDate = record.DiscountGrantedDate,
                LastDiscountUsed = record.LastDiscountUsed,
            };

            await _unitOfWork.UserMembershipRepository.InsertAsync(membership);
            await _unitOfWork.SaveAsync();
            return new ResponseView(true, "Add new user membership successful");
        }
    }
}
