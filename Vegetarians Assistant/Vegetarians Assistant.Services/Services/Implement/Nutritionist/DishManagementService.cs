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
                        Ingredients = dish.Ingredients,
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
        public async Task<DishView?> GetDishByname(string name)
        {

            try
            {
                var dish = (await _unitOfWork.DishRepository.FindAsync(c => c.Name == name)).FirstOrDefault();
                if (dish != null)
                {
                    var dishview = new DishView()
                    {
                        DishId = dish.DishId,
                        Name = dish.Name,
                        Description = dish.Description,
                        ImageUrl= dish.ImageUrl,
                        DishType= dish.DishType,
                        Ingredients= dish.Ingredients,
                        Price = dish.Price,
                        Recipe= dish.Recipe,
                        DietaryPreferenceId = dish.DietaryPreferenceId
                    };
                    return dishview;
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
