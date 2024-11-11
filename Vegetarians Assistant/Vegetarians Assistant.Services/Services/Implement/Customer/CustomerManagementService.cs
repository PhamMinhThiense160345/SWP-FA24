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
    public class CustomerManagementService : ICustomerManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public async Task<bool> IsExistedPhone(string phone)
        {
            try
            {
                bool status = true;
                var existed = (await _unitOfWork.UserRepository.FindAsync(e => e.PhoneNumber == phone)).FirstOrDefault();
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
        public async Task<UserView?> GetUserByUsername(String username)
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
                        Username = user.Username,
                        Weight = user.Weight,
                        ActivityLevel = user.ActivityLevel,
                        Address = user.Address,
                        ImageUrl = user.ImageUrl,
                        Age = user.Age,
                        Gender = user.Gender,
                        Height = user.Height,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        DietaryPreferenceId = user.DietaryPreferenceId,
                        Goal = user.Goal,
                        IsPhoneVerified = user.IsPhoneVerified,
                        Profession = user.Profession
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

        public async Task<bool> CreateUserCustomer(UserView newUser)
        {
            try
            {
                bool status = false;
                newUser.Status = "active";
                newUser.RoleId = 3;
                var user = _mapper.Map<User>(newUser);
                await _unitOfWork.UserRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();
                var insertedUser = (await _unitOfWork.UserRepository.FindAsync(a => a.Email == newUser.Email)).FirstOrDefault();
                if (insertedUser != null)
                {
                    if (newUser.RoleId == 3)
                    {
                        var staff = new User
                        {
                            Username = insertedUser.Username,
                            Email = insertedUser.Email,
                            Address = insertedUser.Address,
                            ImageUrl = insertedUser.ImageUrl,
                            PhoneNumber = insertedUser.PhoneNumber,
                            Age = insertedUser.Age,
                            Gender = insertedUser.Gender,
                            Height = insertedUser.Height,
                            Weight = insertedUser.Weight,
                            ActivityLevel = insertedUser.ActivityLevel,
                            DietaryPreferenceId = insertedUser.DietaryPreferenceId,
                            IsPhoneVerified = insertedUser.IsPhoneVerified,
                            Goal = insertedUser.Goal,
                            Profession = insertedUser.Profession,
                            Password = insertedUser.Password
                        };
                        await _unitOfWork.SaveAsync();
                        status = true;
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                var insertedUser = (await _unitOfWork.UserRepository.FindAsync(a => a.Email == newUser.Email)).FirstOrDefault();
                if (insertedUser != null)
                {
                    await _unitOfWork.UserRepository.DeleteAsync(insertedUser);
                    await _unitOfWork.SaveAsync();
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<DeliveryView?> GetDeliveryInformationByUserId(int id)
        {
            try
            {
                var delivery = await _unitOfWork.UserRepository.GetByIDAsync(id);
                if (delivery != null)
                {
                    var deliveryView = new DeliveryView()
                    {
                        UserId = delivery.UserId,
                        Address = delivery.Address,
                        PhoneNumber = delivery.PhoneNumber,
                        Username = delivery.Username
                    };
                    return deliveryView;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserView?> EditUser(UserView view)
 {
     var user = _mapper.Map<User>(view);
     if (user != null)
     {
         await _unitOfWork.UserRepository.UpdateAsync(user);
         await _unitOfWork.SaveAsync();
         return view;
     }

     return null;
 }

        public async Task<bool> MatchUserNutritionCriteria(int userId)
        {
            try
            {
                // Lấy thông tin của người dùng
                var user = await _unitOfWork.UserRepository.GetByIDAsync(userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                // Tính BMI của người dùng (nếu có chiều cao và cân nặng)
                double? userBmi = null;
                if (user.Height.HasValue && user.Weight.HasValue && user.Height > 0)
                {
                    double heightInMeters = user.Height.Value / 100.0;
                    userBmi = user.Weight.Value / (heightInMeters * heightInMeters);
                }

                // Lấy danh sách tất cả các tiêu chí dinh dưỡng
                var allCriteria = await _unitOfWork.NutritionCriterionRepository.GetAllAsync();

                NutritionCriterion? bestMatchCriterion = null;
                int maxMatchCount = 0;

                // So sánh từng tiêu chí dinh dưỡng
                foreach (var criterion in allCriteria)
                {
                    int matchCount = 0;

                    // So sánh các thuộc tính
                    if (!string.IsNullOrEmpty(criterion.Gender) && criterion.Gender.Equals(user.Gender, StringComparison.OrdinalIgnoreCase))
                        matchCount++;
                    if (!string.IsNullOrEmpty(criterion.AgeRange) && IsInRange(user.Age, criterion.AgeRange))
                        matchCount++;
                    if (!string.IsNullOrEmpty(criterion.BmiRange) && userBmi.HasValue && IsInRange(userBmi.Value, criterion.BmiRange))
                        matchCount++;
                    if (!string.IsNullOrEmpty(criterion.Profession) && criterion.Profession.Equals(user.Profession, StringComparison.OrdinalIgnoreCase))
                        matchCount++;
                    if (!string.IsNullOrEmpty(criterion.ActivityLevel) && criterion.ActivityLevel.Equals(user.ActivityLevel, StringComparison.OrdinalIgnoreCase))
                        matchCount++;
                    if (!string.IsNullOrEmpty(criterion.Goal) && criterion.Goal.Equals(user.Goal, StringComparison.OrdinalIgnoreCase))
                        matchCount++;

                    // Lưu lại tiêu chí tốt nhất
                    if (matchCount > maxMatchCount)
                    {
                        maxMatchCount = matchCount;
                        bestMatchCriterion = criterion;
                    }
                }

                // Nếu tìm thấy tiêu chí phù hợp nhất
                if (bestMatchCriterion != null)
                {
                    // Kiểm tra xem userId đã tồn tại trong bảng UsersNutritionCriterion hay chưa
                    var existingRecord = (await _unitOfWork.UsersNutritionCriterionRepository.FindAsync(x => x.UserId == user.UserId)).FirstOrDefault();

                    if (existingRecord != null)
                    {
                        // Nếu tồn tại, cập nhật lại criteria_id
                        existingRecord.CriteriaId = bestMatchCriterion.CriteriaId;
                        await _unitOfWork.UsersNutritionCriterionRepository.UpdateAsync(existingRecord);
                    }
                    else
                    {
                        // Nếu chưa tồn tại, thêm mới
                        var userNutritionCriterion = new UsersNutritionCriterion
                        {
                            UserId = user.UserId,
                            CriteriaId = bestMatchCriterion.CriteriaId
                        };

                        await _unitOfWork.UsersNutritionCriterionRepository.InsertAsync(userNutritionCriterion);
                    }

                    // Lưu thay đổi vào database
                    await _unitOfWork.SaveAsync();
                    return true;
                }

                return false; // Không tìm thấy tiêu chí phù hợp
            }
            catch (Exception ex)
            {
                throw new Exception($"Error matching user nutrition criteria: {ex.Message}");
            }
        }

        // Hàm kiểm tra giá trị nằm trong khoảng
        private bool IsInRange(int? value, string range)
        {
            if (!value.HasValue || string.IsNullOrEmpty(range)) return false;
            var parts = range.Split('-');
            if (parts.Length != 2) return false;
            if (int.TryParse(parts[0], out var min) && int.TryParse(parts[1], out var max))
            {
                return value.Value >= min && value.Value <= max;
            }
            return false;
        }

        private bool IsInRange(double value, string range)
        {
            if (string.IsNullOrEmpty(range)) return false;
            var parts = range.Split('-');
            if (parts.Length != 2) return false;
            if (double.TryParse(parts[0], out var min) && double.TryParse(parts[1], out var max))
            {
                return value >= min && value <= max;
            }
            return false;
        }
    }
}
