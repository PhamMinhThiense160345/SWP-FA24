using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.Admin
{
    public interface IUserManagementService
    {
        Task<List<UserView>> GetAllUser();
        Task<UserView?> GetUserByUsername(string userName);
        Task<UserView?> GetUserByUserId(int id);
        Task<bool> CreateUserStaff(StaffView newUser);
        Task<bool> IsExistedEmail(string email);
        Task<bool> IsExistedPhone(string phone);
    }
}
