using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.IIngredient
{
    public interface IIngredientManagementService
    {
        Task<List<IngredientInfoView>> GetAllIngredient();
        Task<IngredientInfoView?> GetIngredientByIngredientId(int id);
        Task<List<DishIngredientView?>> GetIngredientByDishId(int id);
    }
}
