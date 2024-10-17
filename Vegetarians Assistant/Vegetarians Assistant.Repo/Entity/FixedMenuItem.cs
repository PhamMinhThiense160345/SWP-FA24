using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class FixedMenuItem
{
    public int Id { get; set; }

    public int? FixedMenuId { get; set; }

    public int? DishId { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual FixedMenu? FixedMenu { get; set; }
}
