using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class DishView
    {
        public int DishId { get; set; }

        public string Name { get; set; } = null!;

        public string? DishType { get; set; }

        public string? Description { get; set; }

        public string? Ingredients { get; set; }

        public string? Recipe { get; set; }

        public string? ImageUrl { get; set; }

        public string? Status { get; set; }
        public int? DietaryPreferenceId { get; set; }

        public decimal? Price { get; set; } //dâdawdwadaw
    }
}
