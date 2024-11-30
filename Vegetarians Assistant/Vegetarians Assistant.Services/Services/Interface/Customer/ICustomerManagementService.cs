using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.Customer
{
    public interface ICustomerManagementService
    {
        Task<bool> IsExistedEmail(string email);
        Task<bool> IsExistedPhone(string phone);
        Task<bool> CreateUserCustomer(UserView newUser);
        Task<UserView?> GetUserByUsername(string userName);
        Task<UserView?> EditUser(UserView view);
        Task<DeliveryView?> GetDeliveryInformationByUserId(int id);
        Task<bool> MatchUserNutritionCriteria(int userId);
        Task<List<TotalNutritionDish>> RecommendDishesForUser(int userId, string dishType);
        Task<UsersNutritionCriterionView?> GetUserNutritionCriteriaByUserId(int id);
        Task<List<TotalNutritionDish>> RecommendMenuLunchForUser(int userId);
        Task<List<TotalNutritionDish>> RecommendMenuBreakfastForUser(int userId);
        Task<List<TotalNutritionDish>> RecommendMenuDinnerForUser(int userId);
    }
}
