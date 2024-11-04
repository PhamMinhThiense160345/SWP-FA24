using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class FavoriteDishView
    {
        public int FavoriteId { get; set; }

        public int UserId { get; set; }

        public int DishId { get; set; }

        public string? DishName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public DateTime? FavoriteDate { get; set; }

    }
}
