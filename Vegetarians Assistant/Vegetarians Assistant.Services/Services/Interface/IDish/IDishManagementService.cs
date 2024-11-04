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
        Task<DishView?> GetDishByDishId(int id);
        Task<bool> UpdateDishDetailByDishId(DishView updateDish);
    }
}
