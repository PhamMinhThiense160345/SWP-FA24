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
    public class MembershipTierService : IMembershipTierService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MembershipTierService( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<MembershipTierView> getCustomerMembershipTier(int tierId)
        {
            var membershipTier = await _unitOfWork.MembershipTierRepository.GetByIDAsync(tierId);
            var view = _mapper.Map<MembershipTierView>(membershipTier);
            return view;
        }

       
    }
}
