using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class CartView
    {
        public int CartId { get; set; }

        public int? UserId { get; set; }

        public int? DishId { get; set; }

        public string? DishName { get; set; } = null!;

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
