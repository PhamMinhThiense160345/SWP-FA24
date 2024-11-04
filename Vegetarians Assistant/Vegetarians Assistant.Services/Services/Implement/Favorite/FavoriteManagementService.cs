using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Favorite;

namespace Vegetarians_Assistant.Services.Services.Implement.Favorite
{
    public class FavoriteManagementService : IFavoriteManagementService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteManagementService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<FavoriteDishView?>> GetAllDishFavoriteByUserId(int id)
        {

            try
            {
                var favorites = await _unitOfWork.FavoriteDishRepository.FindAsync(c => c.UserId == id);
                var favoriteViews = new List<FavoriteDishView>();

                var dishIds = new HashSet<int>();
                foreach (var favorite in favorites)
                {
                    if (favorite.DishId > 0)
                    {
                        dishIds.Add(favorite.DishId);
                    }
                }

                var dishs = await _unitOfWork.DishRepository.GetAsync(dp => dishIds.Contains(dp.DishId));

                var preferenceDictionary = new Dictionary<int, string>();
                foreach (var preference in dishs)
                {
                    preferenceDictionary[preference.DishId] = preference.Name;
                }

                var preferenceDictionary2 = new Dictionary<int, string>();
                foreach (var preference in dishs)
                {
                    preferenceDictionary2[preference.DishId] = preference.ImageUrl;
                }

                foreach (var favorite in favorites)
                {
                    favoriteViews.Add(new FavoriteDishView
                    {
                        FavoriteId = favorite.DishId,
                        UserId = favorite.UserId,
                        FavoriteDate = favorite.FavoriteDate,
                        DishId = favorite.DishId,
                        DishName = preferenceDictionary.ContainsKey(favorite.DishId) ? preferenceDictionary[favorite.DishId] : null,
                        ImageUrl = preferenceDictionary2.ContainsKey(favorite.DishId) ? preferenceDictionary2[favorite.DishId] : null
                    });
                }
                return favoriteViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
