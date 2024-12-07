using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class MenuDishView
    {
        public int MenuDishId { get; set; }

        public int? MenuId { get; set; }

        public int? DishId { get; set; }
    }
}
