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

        public async Task<UsersNutritionCriterionView?> GetUserNutritionCriteriaByUserId(int id)
        {

            try
            {
                var user = (await _unitOfWork.UsersNutritionCriterionRepository.FindAsync(c => c.UserId == id)).FirstOrDefault();
                if (user != null)
                {
                    var usersNutritionCriterionView = new UsersNutritionCriterionView()
                    {
                        UserNutritionCriteriaId = user.UserNutritionCriteriaId,
                        CriteriaId = user.CriteriaId,
                        UserId = user.UserId
                    };
                    return usersNutritionCriterionView;
                }
                return null;
            }
            catch (Exception ex)
            {
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

        public async Task<List<TotalNutritionDish>> RecommendDishesForUser(int userId, string dishType)
        {
            try
            {
                // 1. Lấy CriteriaId từ bảng UsersNutritionCriterion dựa vào userId
                var userCriteria = (await _unitOfWork.UsersNutritionCriterionRepository.FindAsync(x => x.UserId == userId))
                    .FirstOrDefault();
                if (userCriteria == null)
                {
                    bool matched = await MatchUserNutritionCriteria(userId);

                    if (!matched)
                    {
                        throw new Exception("Unable to generate nutrition criteria for the user.");
                    }

                    // Lấy lại tiêu chí sau khi tạo
                    userCriteria = (await _unitOfWork.UsersNutritionCriterionRepository.FindAsync(x => x.UserId == userId))
                        .FirstOrDefault();

                    if (userCriteria == null)
                    {
                        throw new Exception("Failed to retrieve nutrition criteria after generating.");
                    }
                }

                int criteriaId = userCriteria.CriteriaId ?? 0;

                // 2. Lấy thông tin dinh dưỡng từ bảng NutritionCriterion dựa trên CriteriaId
                var nutritionCriteria = await _unitOfWork.NutritionCriterionRepository.GetByIDAsync(criteriaId);
                if (userCriteria == null)
                {
                    bool matched = await MatchUserNutritionCriteria(userId);

                    if (!matched)
                    {
                        throw new Exception("Unable to generate nutrition criteria for the user.");
                    }

                    // Lấy lại tiêu chí sau khi tạo
                    userCriteria = (await _unitOfWork.UsersNutritionCriterionRepository.FindAsync(x => x.UserId == userId))
                        .FirstOrDefault();

                    if (userCriteria == null)
                    {
                        throw new Exception("Failed to retrieve nutrition criteria after generating.");
                    }
                }

                // 3. Lấy danh sách tất cả các món ăn từ bảng TotalNutritionDish
                var dishesByType = await _unitOfWork.DishRepository.FindAsync(d => d.DishType != null && d.DishType.ToLower() == dishType.ToLower());
                if (dishesByType == null || !dishesByType.Any())
                {
                    throw new Exception("No dishes found for the specified DishType.");
                }

                // Lấy danh sách DishId từ các món ăn thuộc DishType
                var dishIds = dishesByType.Select(d => d.DishId).ToList();

                // 4. Lấy danh sách các món ăn từ bảng TotalNutritionDish chỉ thuộc DishType
                var allDishes = (await _unitOfWork.TotalNutritionDishRepository.FindAsync(d => dishIds.Contains(d.DishId))).ToList();

                if (!allDishes.Any())
                {
                    throw new Exception("No nutritional information found for the specified dishes.");
                }

                // 4. So sánh món ăn với tiêu chí dinh dưỡng để tính điểm tương đồng
                var rankedDishes = allDishes.Select(dish =>
                {
                    double similarityScore = 0;

                    // So sánh từng thuộc tính dinh dưỡng
                    if (nutritionCriteria.Calories.HasValue && dish.Calories.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Calories.Value - dish.Calories.Value)));

                    if (nutritionCriteria.Protein.HasValue && dish.Protein.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Protein.Value - dish.Protein.Value)));

                    if (nutritionCriteria.Carbs.HasValue && dish.Carbs.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Carbs.Value - dish.Carbs.Value)));

                    if (nutritionCriteria.Fat.HasValue && dish.Fat.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Fat.Value - dish.Fat.Value)));

                    if (nutritionCriteria.Fiber.HasValue && dish.Fiber.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Fiber.Value - dish.Fiber.Value)));

                    if (nutritionCriteria.VitaminA.HasValue && dish.VitaminA.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminA.Value - dish.VitaminA.Value)));

                    if (nutritionCriteria.VitaminB.HasValue && dish.VitaminB.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminB.Value - dish.VitaminB.Value)));

                    if (nutritionCriteria.VitaminC.HasValue && dish.VitaminC.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminC.Value - dish.VitaminC.Value)));

                    if (nutritionCriteria.VitaminD.HasValue && dish.VitaminD.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminD.Value - dish.VitaminD.Value)));

                    if (nutritionCriteria.VitaminE.HasValue && dish.VitaminE.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminE.Value - dish.VitaminE.Value)));

                    if (nutritionCriteria.Calcium.HasValue && dish.Calcium.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Calcium.Value - dish.Calcium.Value)));

                    if (nutritionCriteria.Iron.HasValue && dish.Iron.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Iron.Value - dish.Iron.Value)));

                    if (nutritionCriteria.Magnesium.HasValue && dish.Magnesium.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Magnesium.Value - dish.Magnesium.Value)));

                    if (nutritionCriteria.Omega3.HasValue && dish.Omega3.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Omega3.Value - dish.Omega3.Value)));

                    if (nutritionCriteria.Sugars.HasValue && dish.Sugars.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Sugars.Value - dish.Sugars.Value)));

                    if (nutritionCriteria.Cholesterol.HasValue && dish.Cholesterol.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Cholesterol.Value - dish.Cholesterol.Value)));

                    if (nutritionCriteria.Sodium.HasValue && dish.Sodium.HasValue)
                        similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Sodium.Value - dish.Sodium.Value)));



                    return new { Dish = dish, Score = similarityScore };
                })
                .OrderByDescending(x => x.Score) // Sắp xếp theo điểm số giảm dần
                .Take(10) // Chọn 20 món có điểm cao nhất
                .Select(x => x.Dish)
                .ToList();

                // 5. Random 10 món từ danh sách 20 món đã chọn
                var random = new Random();
                var recommendedDishes = rankedDishes.OrderBy(x => random.Next()).Take(6).ToList();

                return recommendedDishes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error recommending dishes for user: {ex.Message}");
            }
        }

        public async Task<List<TotalNutritionDish>> RecommendMenuForUser(int userId)
        {
            try
            {
                // Lấy CriteriaId từ bảng UsersNutritionCriterion
                var userCriteria = (await _unitOfWork.UsersNutritionCriterionRepository.FindAsync(x => x.UserId == userId))
                    .FirstOrDefault();
                if (userCriteria == null)
                {
                    bool matched = await MatchUserNutritionCriteria(userId);

                    if (!matched)
                    {
                        throw new Exception("Unable to generate nutrition criteria for the user.");
                    }

                    userCriteria = (await _unitOfWork.UsersNutritionCriterionRepository.FindAsync(x => x.UserId == userId))
                        .FirstOrDefault();

                    if (userCriteria == null)
                    {
                        throw new Exception("Failed to retrieve nutrition criteria after generating.");
                    }
                }

                int criteriaId = userCriteria.CriteriaId ?? 0;

                // Lấy thông tin dinh dưỡng từ bảng NutritionCriterion
                var nutritionCriteria = await _unitOfWork.NutritionCriterionRepository.GetByIDAsync(criteriaId);
                if (nutritionCriteria == null)
                {
                    throw new Exception("No nutrition criteria found for the user.");
                }

                // Định nghĩa danh sách DishType cần tạo menu
                var dishTypes = new List<string> { "Khai vị", "Đồ uống", "Món chính", "Canh", "Tráng miệng" };

                var menu = new List<TotalNutritionDish>();

                foreach (var dishType in dishTypes)
                {
                    // Lấy danh sách món ăn thuộc DishType
                    var dishesByType = await _unitOfWork.DishRepository.FindAsync(d => d.DishType != null && d.DishType.ToLower() == dishType.ToLower());
                    if (dishesByType == null || !dishesByType.Any())
                    {
                        throw new Exception($"No dishes found for the DishType: {dishType}");
                    }

                    var dishIds = dishesByType.Select(d => d.DishId).ToList();

                    var allDishes = (await _unitOfWork.TotalNutritionDishRepository.FindAsync(d => dishIds.Contains(d.DishId))).ToList();

                    if (!allDishes.Any())
                    {
                        throw new Exception($"No nutritional information found for the DishType: {dishType}");
                    }

                    // Tính điểm tương đồng cho từng món ăn
                    var rankedDishes = allDishes.Select(dish =>
                    {
                        double similarityScore = 0;

                        if (nutritionCriteria.Calories.HasValue && dish.Calories.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Calories.Value - dish.Calories.Value)));

                        if (nutritionCriteria.Protein.HasValue && dish.Protein.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Protein.Value - dish.Protein.Value)));

                        if (nutritionCriteria.Carbs.HasValue && dish.Carbs.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Carbs.Value - dish.Carbs.Value)));

                        if (nutritionCriteria.Fat.HasValue && dish.Fat.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Fat.Value - dish.Fat.Value)));

                        if (nutritionCriteria.Fiber.HasValue && dish.Fiber.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Fiber.Value - dish.Fiber.Value)));

                        if (nutritionCriteria.VitaminA.HasValue && dish.VitaminA.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminA.Value - dish.VitaminA.Value)));

                        if (nutritionCriteria.VitaminB.HasValue && dish.VitaminB.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminB.Value - dish.VitaminB.Value)));

                        if (nutritionCriteria.VitaminC.HasValue && dish.VitaminC.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminC.Value - dish.VitaminC.Value)));

                        if (nutritionCriteria.VitaminD.HasValue && dish.VitaminD.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminD.Value - dish.VitaminD.Value)));

                        if (nutritionCriteria.VitaminE.HasValue && dish.VitaminE.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.VitaminE.Value - dish.VitaminE.Value)));

                        if (nutritionCriteria.Calcium.HasValue && dish.Calcium.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Calcium.Value - dish.Calcium.Value)));

                        if (nutritionCriteria.Iron.HasValue && dish.Iron.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Iron.Value - dish.Iron.Value)));

                        if (nutritionCriteria.Magnesium.HasValue && dish.Magnesium.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Magnesium.Value - dish.Magnesium.Value)));

                        if (nutritionCriteria.Omega3.HasValue && dish.Omega3.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Omega3.Value - dish.Omega3.Value)));

                        if (nutritionCriteria.Sugars.HasValue && dish.Sugars.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Sugars.Value - dish.Sugars.Value)));

                        if (nutritionCriteria.Cholesterol.HasValue && dish.Cholesterol.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Cholesterol.Value - dish.Cholesterol.Value)));

                        if (nutritionCriteria.Sodium.HasValue && dish.Sodium.HasValue)
                            similarityScore += 1 / (1 + Math.Abs((double)(nutritionCriteria.Sodium.Value - dish.Sodium.Value)));

                        return new { Dish = dish, Score = similarityScore };
                    })
                    .OrderByDescending(x => x.Score) // Sắp xếp theo điểm số
                    .Take(5) // Lấy tối đa 5 món ăn phù hợp nhất
                    .Select(x => x.Dish)
                    .ToList();

                    // Random một món từ danh sách đã chọn
                    if (rankedDishes.Any())
                    {
                        var random = new Random();
                        var selectedDish = rankedDishes[random.Next(rankedDishes.Count)];
                        menu.Add(selectedDish);
                    }
                }

                return menu;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error recommending menu for user: {ex.Message}");
            }
        }
    }
}
