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
using Vegetarians_Assistant.Services.Services.Interface.Favorite;
using static Vegetarians_Assistant.Services.Enum.Enum;

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

        public async Task<bool> CreateFavoriteDish(FavoriteView newFavorite)
        {
            try
            {
                bool status = false;
                
                var favorite = _mapper.Map<FavoriteDish>(newFavorite);
                await _unitOfWork.FavoriteDishRepository.InsertAsync(favorite);
                await _unitOfWork.SaveAsync();
                var insertedFavorite = (await _unitOfWork.FavoriteDishRepository.FindAsync(a => a.DishId == newFavorite.DishId)).FirstOrDefault();

                if (insertedFavorite != null)
                {
                    var favo = new FavoriteDish
                    {
                        FavoriteId = insertedFavorite.FavoriteId,
                        DishId= newFavorite.DishId,
                        FavoriteDate = DateTime.Now,
                        UserId = newFavorite.DishId
                    };
                    await _unitOfWork.SaveAsync();
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                var insertedFavorite = (await _unitOfWork.FavoriteDishRepository.FindAsync(a => a.FavoriteId == newFavorite.FavoriteId)).FirstOrDefault();
                if (insertedFavorite != null)
                {
                    await _unitOfWork.FavoriteDishRepository.DeleteAsync(insertedFavorite);
                    await _unitOfWork.SaveAsync();
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFavoriteDish(FavoriteView deleteFavorite)
        {
            try
            {
                bool status = false;
                var favorite = _mapper.Map<FavoriteDish>(deleteFavorite);
                var insertedFavorite = (await _unitOfWork.FavoriteDishRepository.FindAsync(a => a.DishId == deleteFavorite.DishId && a.UserId == deleteFavorite.UserId)).FirstOrDefault();
                if (insertedFavorite != null)
                {
                    await _unitOfWork.FavoriteDishRepository.DeleteAsync(insertedFavorite);
                    await _unitOfWork.SaveAsync();
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
