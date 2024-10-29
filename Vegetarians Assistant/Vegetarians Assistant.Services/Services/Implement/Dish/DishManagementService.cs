using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Nutritionist;

namespace Vegetarians_Assistant.Services.Services.Implement.Nutritionist
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
                foreach (Dish dish in dishs)
                {
                    var dishView = new DishView()
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        DietaryPreferenceId = dish.DietaryPreferenceId,
                        DishType = dish.DishType,
                        ImageUrl = dish.ImageUrl,
                        Price = dish.Price,
                        Recipe = dish.Recipe
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
                foreach (var dish in dishes)
                {
                    dishViews.Add(new DishView
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        DietaryPreferenceId = dish.DietaryPreferenceId,
                        DishType = dish.DishType,
                        ImageUrl = dish.ImageUrl,
                        Price = dish.Price,
                        Recipe = dish.Recipe
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
                foreach (var dish in dishes)
                {
                    dishViews.Add(new DishView
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        DietaryPreferenceId = dish.DietaryPreferenceId,
                        DishType = dish.DishType,
                        ImageUrl = dish.ImageUrl,
                        Price = dish.Price,
                        Recipe = dish.Recipe
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
                    var dishView = new DishView()
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        ImageUrl= dish.ImageUrl,
                        DietaryPreferenceId= dish.DietaryPreferenceId,
                        DishType = dish.DishType,
                        Price = dish.Price,
                        Recipe= dish.Recipe
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
