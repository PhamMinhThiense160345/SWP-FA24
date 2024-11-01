using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;

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
    }
}
