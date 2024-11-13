using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IIngredient;

namespace Vegetarians_Assistant.Services.Services.Implement.IngredientImp
{
    public class IngredientManagementService : IIngredientManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IngredientManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<IngredientInfoView>> GetAllIngredient()
        {
            try
            {
                var ingredients = (await _unitOfWork.IngredientRepository.GetAsync()).ToList();
                List<IngredientInfoView> ingredientInfoViews = new List<IngredientInfoView>();

                foreach (var ingredient in ingredients)
                {
                    var ingredientInfoView = new IngredientInfoView()
                    {
                        IngredientId = ingredient.IngredientId,
                        Name = ingredient.Name,
                        Weight = ingredient.Weight,
                        Calories = ingredient.Calories,
                        Protein = ingredient.Protein,
                        Carbs = ingredient.Carbs,
                        Fat = ingredient.Fat,
                        Fiber = ingredient.Fiber,
                        VitaminA = ingredient.VitaminA,
                        VitaminB = ingredient.VitaminB,
                        VitaminC = ingredient.VitaminC,
                        VitaminD = ingredient.VitaminD,
                        VitaminE = ingredient.VitaminE,
                        Calcium = ingredient.Calcium,
                        Iron = ingredient.Iron,
                        Magnesium = ingredient.Magnesium,
                        Omega3 = ingredient.Omega3,
                        Sugars = ingredient.Sugars,
                        Cholesterol = ingredient.Cholesterol,
                        Sodium = ingredient.Sodium
                    };
                    ingredientInfoViews.Add(ingredientInfoView);
                }
                return ingredientInfoViews;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IngredientInfoView?> GetIngredientByIngredientId(int id)
        {
            try
            {
                var ingredient = await _unitOfWork.IngredientRepository.GetByIDAsync(id);
                if (ingredient != null)
                {
                    var ingredientInfoView = new IngredientInfoView()
                    {
                        IngredientId = ingredient.IngredientId,
                        Name = ingredient.Name,
                        Weight = ingredient.Weight,
                        Calories = ingredient.Calories,
                        Protein = ingredient.Protein,
                        Carbs = ingredient.Carbs,
                        Fat = ingredient.Fat,
                        Fiber = ingredient.Fiber,
                        VitaminA = ingredient.VitaminA,
                        VitaminB = ingredient.VitaminB,
                        VitaminC = ingredient.VitaminC,
                        VitaminD = ingredient.VitaminD,
                        VitaminE = ingredient.VitaminE,
                        Calcium = ingredient.Calcium,
                        Iron = ingredient.Iron,
                        Magnesium = ingredient.Magnesium,
                        Omega3 = ingredient.Omega3,
                        Sugars = ingredient.Sugars,
                        Cholesterol = ingredient.Cholesterol,
                        Sodium = ingredient.Sodium
                    };
                    return ingredientInfoView;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DishIngredientView?>> GetIngredientByDishId(int id)
        {

            try
            {
                var dishIngredients = await _unitOfWork.DishIngredientRepository.FindAsync(c => c.DishId == id);
                var dishIngredientViews = new List<DishIngredientView>();

                foreach (var dishIngredient in dishIngredients)
                {
                    dishIngredientViews.Add(new DishIngredientView
                    {
                        DishIngredientId = dishIngredient.DishIngredientId,
                        DishId = dishIngredient.DishId,
                        IngredientId = dishIngredient.IngredientId,
                        Weight = dishIngredient.Weight
                    });
                }
                return dishIngredientViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
