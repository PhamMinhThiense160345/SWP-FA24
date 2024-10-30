using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.Services.Interface.Membership
{
    public interface IMembershipTierService
    {
        Task<Repo.Entity.MembershipTierView> getCustomerMembershipTier(int tierId);
    }
}
