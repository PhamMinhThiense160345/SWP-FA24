using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class DishIngredientView
    {
        public int DishIngredientId { get; set; }

        public int? DishId { get; set; }

        public int? IngredientId { get; set; }

        public decimal? Weight { get; set; }

    }
}
