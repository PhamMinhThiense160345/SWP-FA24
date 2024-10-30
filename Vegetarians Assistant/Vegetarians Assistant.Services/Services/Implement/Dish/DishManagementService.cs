using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Dish;

namespace Vegetarians_Assistant.Services.Services.Implement.Dish
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

                // Lấy danh sách DietaryPreferences từ repository
                var dietaryPreferences = await _unitOfWork.DietaryPreferenceRepository.GetAsync(dp => dietaryPreferenceIds.Contains(dp.Id));

                // Tạo dictionary để tra cứu PreferenceName theo DietaryPreferenceId
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
                var dishes = await _unitOfWork.DishRepository.GetAsync(c => c.DishType == dishType);
                var dishViews = new List<DishView>();

                var dietaryPreferenceIds = new HashSet<int>();
                foreach (var dish in dishes)
                {
                    if (dish.DietaryPreferenceId.HasValue)
                    {
                        dietaryPreferenceIds.Add(dish.DietaryPreferenceId.Value);
                    }
                }

                // Lấy danh sách DietaryPreferences từ repository
                var dietaryPreferences = await _unitOfWork.DietaryPreferenceRepository.GetAsync(dp => dietaryPreferenceIds.Contains(dp.Id));

                // Tạo dictionary để tra cứu PreferenceName theo DietaryPreferenceId
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
                var dishes = await _unitOfWork.DishRepository.FindAsync(c => c.Name.Contains(name));
                var dishViews = new List<DishView>();

                var dietaryPreferenceIds = new HashSet<int>();
                foreach (var dish in dishes)
                {
                    if (dish.DietaryPreferenceId.HasValue)
                    {
                        dietaryPreferenceIds.Add(dish.DietaryPreferenceId.Value);
                    }
                }

                // Lấy danh sách DietaryPreferences từ repository
                var dietaryPreferences = await _unitOfWork.DietaryPreferenceRepository.GetAsync(dp => dietaryPreferenceIds.Contains(dp.Id));

                // Tạo dictionary để tra cứu PreferenceName theo DietaryPreferenceId
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
    }
}
