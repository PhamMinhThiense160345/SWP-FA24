using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Customer;

namespace Vegetarians_Assistant.Services.Services.Implement.Customer
{
    public class LoginCService : ILoginCService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LoginCService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserView?> Login(LoginView loginInfo)
        {
            try
            {
                User? user = (await _unitOfWork.UserRepository.GetAsync(a => a.PhoneNumber == loginInfo.PhoneNumber)).FirstOrDefault();
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginInfo.Password, user.Password))
                {
                    // Trả về null nếu không tìm thấy hoặc mật khẩu không khớp
                    return null;
                }
                //User? user = (await _unitOfWork.UserRepository.GetAsync(a => a.PhoneNumber == loginInfo.PhoneNumber && a.Password == loginInfo.Password)).FirstOrDefault();
                //if (user == null)
                //{
                //    return null;
                //}
                var userExisted = (await _unitOfWork.UserRepository.GetAsync(c => c.UserId == user.UserId)).FirstOrDefault();
                if (userExisted == null)
                {
                    return null;
                }
                UserView? userView = new UserView()
                {
                    UserId = user.UserId,
                    PhoneNumber = user.PhoneNumber,
                    Password = user.Password,
                    ActivityLevel = user.ActivityLevel,
                    Address = user.Address,
                    Age = user.Age,
                    DietaryPreferenceId = user.DietaryPreferenceId,
                    Email = user.Email,
                    Gender = user.Gender,
                    Goal = user.Goal,
                    Height = user.Height,
                    ImageUrl = user.ImageUrl,
                    IsPhoneVerified = user.IsPhoneVerified,
                    Profession = user.Profession,
                    RoleId = user.RoleId,
                    Status = user.Status,
                    Username = user.Username,
                    Weight = user.Weight

                };
                return userView;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xác thực người dùng: {ex.Message}", ex);
            }
        }
    }
}
