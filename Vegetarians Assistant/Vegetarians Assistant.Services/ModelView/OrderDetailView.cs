﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class OrderDetailView
    {
        public int OrderDetailId { get; set; }

        public int? OrderId { get; set; }

        public int? DishId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

    }
}
