﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.Favorite
{
    public interface IFavoriteManagementService
    {
        Task<List<FavoriteDishView?>> GetAllDishFavoriteByUserId(int id);
        Task<bool> CreateFavoriteDish(FavoriteView newFavorite);
        Task<bool> DeleteFavoriteDish(FavoriteView deleteFavorite);
    }
}
