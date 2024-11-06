using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.INutritionCriterion
{
    public interface INutritionCriterionManagementService
    {
        Task<List<NutritionCriterionView>> GetAllNutritionCriteria();
        Task<bool> CreateNutritionCriteria(NutritionCriterionView newNutritionCriteria);
    }
}
