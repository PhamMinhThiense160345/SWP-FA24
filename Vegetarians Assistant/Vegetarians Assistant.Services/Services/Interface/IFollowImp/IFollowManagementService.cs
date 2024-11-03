using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.IFollowImp
{
    public interface IFollowManagementService
    {
        Task<List<FollowerView?>> GetAllFollowerByUserId(int userId);
        Task<List<FollowingView?>> GetAllFollowingByUserId(int userId);

        Task<bool> FollowingUserByCustomer(FollowingView newFollow);
        Task<bool> FollowerUserByCustomer(FollowerView newFollow);
    }
}
