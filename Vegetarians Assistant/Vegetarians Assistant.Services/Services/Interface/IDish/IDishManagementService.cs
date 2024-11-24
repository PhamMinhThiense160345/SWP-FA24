using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.Dish
{
    public interface IDishManagementService
    {
        Task<List<DishView>> GetAllDish();
        Task<List<DishView?>> GetDishByname(string name);
        Task<List<DishView?>> GetDishByDishType(string dishType);
        Task<List<DishView?>> GetDishByDietaryPreferenceId(int id);
        Task<List<TotalNutritionDishView?>> GetTotalNutritionDishByDishId(int id);
        Task<List<DishView>> GetDishByIngredientName(string ingredientName);
        Task<DishView?> GetDishByDishId(int id);
        Task<bool> UpdateDishDetailByDishId(DishView updateDish);
        Task<DishNutritionalView?> CalculateNutrition(int dishId);
        Task<ResponseView> AddIngredientAsync(AddIngredientView request);
        Task<ResponseView> UpdateIngredientAsync(UpdateIngredientView request);
         Task<ResponseView> RemoveIngredientAsync(int dishId, int ingredientId);
    }
}
