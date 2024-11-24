using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class MenuDish
{
    public int MenuDishId { get; set; }

    public int? MenuId { get; set; }

    public int? DishId { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual Menu? Menu { get; set; }
}
