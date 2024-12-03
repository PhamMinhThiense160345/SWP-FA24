using AutoMapper;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;
using Vegetarians_Assistant.Services.Services.Implement; // Thêm tham chiếu đến AuthService

namespace Vegetarians_Assistant.Services.Services.Implement.Admin
{
    public class LoginAService : ILoginAService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly AuthService _authService;

        public LoginAService(IUnitOfWork unitOfWork, IMapper mapper)//, AuthService authService
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            //this._authService = authService;
        }

        public async Task<UserView?> Login(LoginView loginInfo)
        {
            try
            {
                User? user = (await _unitOfWork.UserRepository.GetAsync(a => a.PhoneNumber == loginInfo.PhoneNumber)).FirstOrDefault();
                //if (user == null || !BCrypt.Net.BCrypt.Verify(loginInfo.Password, user.Password))
                //{
                //    // Trả về null nếu không tìm thấy hoặc mật khẩu không khớp
                //    return null;
                //}
                //User? user = (await _unitOfWork.UserRepository.GetAsync(a => a.PhoneNumber == loginInfo.PhoneNumber && a.Password == loginInfo.Password)).FirstOrDefault();
                //if (user == null)
                //{
                //    return null;
                //}
                if(user == null)
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

        public async Task<bool> IsExistedEmail(string email)
        {
            try
            {
                var existed = (await _unitOfWork.UserRepository.FindAsync(e => e.Email == email)).FirstOrDefault();
                return existed != null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra email tồn tại: {ex.Message}", ex);
            }
        }

        // Phương thức mã hóa mật khẩu
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
