using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class DishIngredient
{
    public int DishIngredientId { get; set; }

    public int? DishId { get; set; }

    public int? IngredientId { get; set; }

    public decimal? Weight { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual Ingredient? Ingredient { get; set; }
}
