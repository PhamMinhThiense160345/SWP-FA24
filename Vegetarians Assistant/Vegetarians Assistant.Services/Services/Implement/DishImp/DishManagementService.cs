using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Implement;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Dish;

namespace Vegetarians_Assistant.Services.Services.Implement.DishImp
{
    public class DishManagementService : IDishManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DishManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<DishView>> GetAllDish()
        {
            try
            {
                var dishs = (await _unitOfWork.DishRepository.GetAsync()).ToList();
                List<DishView> dishViews = new List<DishView>();

                var dietaryPreferenceIds = new HashSet<int>();
                foreach (var dish in dishs)
                {
                    if (dish.DietaryPreferenceId.HasValue)
                    {
                        dietaryPreferenceIds.Add(dish.DietaryPreferenceId.Value);
                    }
                }

                var dietaryPreferences = await _unitOfWork.DietaryPreferenceRepository.GetAsync(dp => dietaryPreferenceIds.Contains(dp.Id));

                var preferenceDictionary = new Dictionary<int, string>();
                foreach (var preference in dietaryPreferences)
                {
                    preferenceDictionary[preference.Id] = preference.PreferenceName;
                }

                foreach (var dish in dishs)
                {
                    var dishView = new DishView()
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        DishType = dish.DishType,
                        ImageUrl = dish.ImageUrl,
                        Price = dish.Price,
                        Recipe = dish.Recipe,
                        Status = dish.Status,
                        DietaryPreferenceId = dish.DietaryPreferenceId,
                        PreferenceName = dish.DietaryPreferenceId.HasValue && preferenceDictionary.ContainsKey(dish.DietaryPreferenceId.Value)
                    ? preferenceDictionary[dish.DietaryPreferenceId.Value]
                    : null
                    };
                    dishViews.Add(dishView);
                }
                return dishViews;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DishView?>> GetDishByDishType(string dishType)
        {

            try
            {
                var dishes = await _unitOfWork.DishRepository.FindAsync(c => c.DishType == dishType);
                var dishViews = new List<DishView>();

                var dietaryPreferenceIds = new HashSet<int>();
                foreach (var dish in dishes)
                {
                    if (dish.DietaryPreferenceId.HasValue)
                    {
                        dietaryPreferenceIds.Add(dish.DietaryPreferenceId.Value);
                    }
                }
                var dietaryPreferences = await _unitOfWork.DietaryPreferenceRepository.GetAsync(dp => dietaryPreferenceIds.Contains(dp.Id));

                var preferenceDictionary = new Dictionary<int, string>();
                foreach (var preference in dietaryPreferences)
                {
                    preferenceDictionary[preference.Id] = preference.PreferenceName;
                }

                foreach (var dish in dishes)
                {
                    dishViews.Add(new DishView
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        DishType = dish.DishType,
                        ImageUrl = dish.ImageUrl,
                        Price = dish.Price,
                        Recipe = dish.Recipe,
                        Status = dish.Status,
                        DietaryPreferenceId = dish.DietaryPreferenceId,
                        PreferenceName = dish.DietaryPreferenceId.HasValue && preferenceDictionary.ContainsKey(dish.DietaryPreferenceId.Value)
                    ? preferenceDictionary[dish.DietaryPreferenceId.Value]
                    : null
                    });
                }
                return dishViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DishView?>> GetDishByname(string name)
        {

            try
            {
                var dishes = await _unitOfWork.DishRepository.GetAsync(c => c.Name.Contains(name));
                var dishViews = new List<DishView>();

                var dietaryPreferenceIds = new HashSet<int>();
                foreach (var dish in dishes)
                {
                    if (dish.DietaryPreferenceId.HasValue)
                    {
                        dietaryPreferenceIds.Add(dish.DietaryPreferenceId.Value);
                    }
                }
                var dietaryPreferences = await _unitOfWork.DietaryPreferenceRepository.GetAsync(dp => dietaryPreferenceIds.Contains(dp.Id));

                var preferenceDictionary = new Dictionary<int, string>();
                foreach (var preference in dietaryPreferences)
                {
                    preferenceDictionary[preference.Id] = preference.PreferenceName;
                }

                foreach (var dish in dishes)
                {
                    dishViews.Add(new DishView
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        DishType = dish.DishType,
                        ImageUrl = dish.ImageUrl,
                        Price = dish.Price,
                        Recipe = dish.Recipe,
                        Status = dish.Status,
                        DietaryPreferenceId = dish.DietaryPreferenceId,
                        PreferenceName = dish.DietaryPreferenceId.HasValue && preferenceDictionary.ContainsKey(dish.DietaryPreferenceId.Value)
                    ? preferenceDictionary[dish.DietaryPreferenceId.Value]
                    : null
                    });
                }
                return dishViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DishView?> GetDishByDishId(int id)
        {
            try
            {
                var dish = await _unitOfWork.DishRepository.GetByIDAsync(id);
                if (dish != null)
                {
                    string? preferenceName = null;
                    if (dish.DietaryPreferenceId.HasValue)
                    {
                        var dietaryPreference = await _unitOfWork.DietaryPreferenceRepository.GetByIDAsync(dish.DietaryPreferenceId.Value);
                        preferenceName = dietaryPreference?.PreferenceName;
                    }

                    var dishView = new DishView()
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        ImageUrl= dish.ImageUrl,
                        DishType = dish.DishType,
                        Price = dish.Price,
                        Recipe= dish.Recipe,
                        Status = dish.Status,
                        DietaryPreferenceId = dish.DietaryPreferenceId,
                        PreferenceName = preferenceName
                    };
                    return dishView;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateDishDetailByDishId(DishView updateDish)
        {
            try
            {
                bool status = false;
                var dish = _mapper.Map<Dish>(updateDish);
                await _unitOfWork.DishRepository.UpdateAsync(dish);
                await _unitOfWork.SaveAsync();
                var exsitDish = (await _unitOfWork.DishRepository.FindAsync(c => c.DishId == updateDish.DishId)).FirstOrDefault();

                if (exsitDish != null)
                {
                    var favo = new Dish
                    {
                        DishId = updateDish.DishId,
                        Description = exsitDish.Description,
                        DietaryPreferenceId = exsitDish.DietaryPreferenceId,
                        DishType = exsitDish.DishType,
                        ImageUrl = exsitDish.ImageUrl,
                        Name = exsitDish.Name,
                        Price = exsitDish.Price,
                        Recipe = exsitDish.Recipe,
                        Status = exsitDish.Status,
                    };
                    await _unitOfWork.SaveAsync();
                    status = true;
                }
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResponseView> AddIngredientAsync(AddIngredientView request)
        {
            try
            {
                if (request.Weight <= 0) throw new Exception("Weight must be greater than 0");

                var ingredient = await _unitOfWork.IngredientRepository.GetByIDAsync(request.IngredientId);
                if (ingredient is null) throw new Exception("Not found ingredient with id = " + request.IngredientId);

                var dish = await _unitOfWork.DishRepository.GetByIDAsync(request.DishId);
                if (dish is null) throw new Exception("Not found dish with id = " + request.DishId);

                var searchResult = await _unitOfWork.DishIngredientRepository
                  .FindAsync(x => x.DishId == request.DishId && x.IngredientId == request.IngredientId);

                var current = searchResult.FirstOrDefault();
                if (current is not null) throw new Exception("Ingredient is already exist in the dish");

                var dishIngredient = new DishIngredient()
                {
                    DishId = request.DishId,
                    IngredientId = request.IngredientId,
                    Weight = request.Weight,
                };

                await _unitOfWork.DishIngredientRepository.InsertAsync(dishIngredient);
                return new ResponseView(true, "Add new ingredient to dish successfully");
            }
            catch (Exception ex)
            {
                return new ResponseView(false, ex.Message);
            }
        }
        public async Task<ResponseView> UpdateIngredientAsync(UpdateIngredientView request)
        {
            try
            {
                if (request.NewWeight <= 0) throw new Exception("Weight must be greater than 0");

                var ingredient = await _unitOfWork.IngredientRepository.GetByIDAsync(request.IngredientId);
                if (ingredient is null) throw new Exception("Not found ingredient with id = " + request.IngredientId);

                var dish = await _unitOfWork.DishRepository.GetByIDAsync(request.DishId);
                if (dish is null) throw new Exception("Not found dish with id = " + request.DishId);


                var searchResult = await _unitOfWork.DishIngredientRepository
                  .FindAsync(x => x.DishId == request.DishId && x.IngredientId == request.IngredientId);

                var current = searchResult.FirstOrDefault();
                if (current is null) throw new Exception("Not found dish ingredient");

                current.Weight = request.NewWeight;

                await _unitOfWork.DishIngredientRepository.UpdateAsync(current);
                return new ResponseView(true, "Update ingredient weight in dish successfully");
            }
            catch (Exception ex)
            {
                return new ResponseView(false, ex.Message);
            }
        }
         public async Task<ResponseView> RemoveIngredientAsync(int dishId, int ingredientId)
 {
     try
     {
         var ingredient = await _unitOfWork.IngredientRepository.GetByIDAsync(ingredientId);
         if (ingredient is null) throw new Exception("Not found ingredient with id = " + ingredientId);

         var dish = await _unitOfWork.DishRepository.GetByIDAsync(dishId);
         if (dish is null) throw new Exception("Not found dish with id = " + dishId);


         var searchResult = await _unitOfWork.DishIngredientRepository
             .FindAsync(x => x.DishId == dishId && x.IngredientId == ingredientId);

         var current = searchResult.FirstOrDefault();
         if (current is null) throw new Exception("Not found dish ingredient");


         await _unitOfWork.DishIngredientRepository.DeleteAsync(current);
         return new ResponseView(true, "Remove ingredient from dish successfully");
     }
     catch (Exception ex)
     {
         return new ResponseView(false, ex.Message);
     }
 }

        public async Task<DishNutritionalView?> CalculateNutrition(int dishId)
        {
            var dish = await _unitOfWork.DishRepository.GetByIDAsync(dishId);
            if (dish is null) return null;

            // Fetch all ingredient IDs and their corresponding weights for the specified dish
            var query = await _unitOfWork.DishIngredientRepository
                .FindAsync(x => x.DishId == dishId);

            var dishIngredients = query.ToList();

            // Initialize totals
            var nutritionalInfo = new DishNutritionalView { Name = dish.Name };

            foreach (var dishIngredient in dishIngredients)
            {
                var ingredient = await _unitOfWork.IngredientRepository.GetByIDAsync(dishIngredient.IngredientId!);

                if (ingredient != null)
                {
                    // Calculate nutritional values based on the weight of the ingredient in the dish
                    var weightRatio = dishIngredient.Weight / ingredient.Weight;

                    nutritionalInfo.TotalWeights += dishIngredient.Weight;
                    nutritionalInfo.TotalCalories += ingredient.Calories * weightRatio;
                    nutritionalInfo.TotalProtein += ingredient.Protein * weightRatio;
                    nutritionalInfo.TotalCarbs += ingredient.Carbs * weightRatio;
                    nutritionalInfo.TotalFat += ingredient.Fat * weightRatio;
                    nutritionalInfo.TotalFiber += ingredient.Fiber * weightRatio;
                    nutritionalInfo.TotalVitaminA += ingredient.VitaminA * weightRatio;
                    nutritionalInfo.TotalVitaminB += ingredient.VitaminB * weightRatio;
                    nutritionalInfo.TotalVitaminC += ingredient.VitaminC * weightRatio;
                    nutritionalInfo.TotalVitaminD += ingredient.VitaminD * weightRatio;
                    nutritionalInfo.TotalVitaminE += ingredient.VitaminE * weightRatio;
                    nutritionalInfo.TotalCalcium += ingredient.Calcium * weightRatio;
                    nutritionalInfo.TotalIron += ingredient.Iron * weightRatio;
                    nutritionalInfo.TotalMagnesium += ingredient.Magnesium * weightRatio;
                    nutritionalInfo.TotalOmega3 += ingredient.Omega3 * weightRatio;
                    nutritionalInfo.TotalSugars += ingredient.Sugars * weightRatio;
                    nutritionalInfo.TotalCholesterol += ingredient.Cholesterol * weightRatio;
                    nutritionalInfo.TotalSodium += ingredient.Sodium * weightRatio;
                }
            }


            var exist = await _unitOfWork.TotalNutritionDishRepository.GetByIDAsync(dishId);
            var record = MapViewModelToEntity(nutritionalInfo, dishId);

            if (exist is null)
            {
                await _unitOfWork.TotalNutritionDishRepository.InsertAsync(record);
            }
            else
            {
                await _unitOfWork.TotalNutritionDishRepository.DeleteAsync(exist);
                await _unitOfWork.TotalNutritionDishRepository.InsertAsync(record);
            }


            return nutritionalInfo;
        }
        public TotalNutritionDish MapViewModelToEntity(DishNutritionalView viewModel, int dishId)
        {
            return new TotalNutritionDish
            {
                DishId = dishId,
                TotalWeight = viewModel.TotalWeights,
                DishName = viewModel.Name,
                Calories = viewModel.TotalCalories,
                Protein = viewModel.TotalProtein,
                Carbs = viewModel.TotalCarbs,
                Fat = viewModel.TotalFat,
                Fiber = viewModel.TotalFiber,
                VitaminA = viewModel.TotalVitaminA,
                VitaminB = viewModel.TotalVitaminB,
                VitaminC = viewModel.TotalVitaminC,
                VitaminD = viewModel.TotalVitaminD,
                VitaminE = viewModel.TotalVitaminE,
                Calcium = viewModel.TotalCalcium,
                Iron = viewModel.TotalIron,
                Magnesium = viewModel.TotalMagnesium,
                Omega3 = viewModel.TotalOmega3,
                Sugars = viewModel.TotalSugars,
                Cholesterol = viewModel.TotalCholesterol,
                Sodium = viewModel.TotalSodium,
            };
        }


    }
}
