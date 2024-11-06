using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class OrderDetailInfo
    {
        public int OrderDetailId { get; set; }

        public int? OrderId { get; set; }

        public int? DishId { get; set; }

        public string? DishName { get; set; } = null!;

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }
    }
}
