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
    public class LoginAService : ILoginAService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LoginAService(IUnitOfWork unitOfWOrk, IMapper mapper)
        {
            this._unitOfWork = unitOfWOrk;
            this._mapper = mapper;
        }

        public async Task<UserView?> AuthenticateUser(LoginView loginInfo)
        {

            try
            {
                User? user = (await _unitOfWork.UserRepository.FindAsync(a => a.Email == loginInfo.Email && a.Password == loginInfo.Password)).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                UserView? userView = new UserView()
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Password = user.Password,
                    Username = user.Username,
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
                    PhoneNumber = user.PhoneNumber,
                    Profession = user.Profession,
                    RoleId = user.RoleId,
                    Status = user.Status,
                    Weight = user.Weight
                };
                return userView;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExistedEmail(string email)
        {
            try
            {
                bool status = true;
                var existed = (await _unitOfWork.UserRepository.FindAsync(e => e.Email == email)).FirstOrDefault();
                if (existed == null)
                {
                    status = false;
                }
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
