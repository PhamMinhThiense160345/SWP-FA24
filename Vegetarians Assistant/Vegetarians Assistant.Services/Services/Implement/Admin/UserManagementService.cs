using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;

namespace Vegetarians_Assistant.Services.Services.Implement.Admin
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<UserView>> GetAllUser()
        {
            try
            {
                var users = (await _unitOfWork.UserRepository.GetAsync()).ToList();
                List<UserView> userViews = new List<UserView>();
                foreach (User user in users)
                {
                    var userView = new UserView()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        ActivityLevel = user.ActivityLevel,
                        Address = user.Address,
                        Age = user.Age,
                        DietaryPreferenceId = user.DietaryPreferenceId,
                        Fullname = user.Fullname,
                        Gender = user.Gender,
                        Height = user.Height,
                        ImageUrl = user.ImageUrl,
                        IsEmailVerified = user.IsEmailVerified,
                        IsPhoneVerified = user.IsPhoneVerified,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Profession = user.Profession,
                        RoleId = user.RoleId,
                        Status = user.Status,
                        Username = user.Username,
                        Weight = user.Weight
                    };
                    userViews.Add(userView);
                }
                return userViews;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserView?> GetCustomerByUsername(String username)
        {

            try
            {
                var user = (await _unitOfWork.UserRepository.FindAsync(c => c.Username == username)).FirstOrDefault();
                if (user != null)
                {
                    var userView = new UserView()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Status = user.Status,
                        Username= user.Username,
                        Weight = user.Weight,
                        ActivityLevel = user.ActivityLevel,
                        Address = user.Address,
                        Age = user.Age,
                        DietaryPreferenceId = user.DietaryPreferenceId,
                        Fullname = user.Fullname,
                        Gender = user.Gender,
                        Height = user.Height,
                        ImageUrl = user.ImageUrl,
                        IsEmailVerified = user.IsEmailVerified,
                        IsPhoneVerified = user.IsPhoneVerified,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Profession = user.Profession,
                        RoleId = user.RoleId
                    };
                    return userView;
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
