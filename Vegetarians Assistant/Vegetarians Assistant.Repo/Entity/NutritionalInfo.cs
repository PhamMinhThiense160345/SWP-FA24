using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class NutritionalInfo
{
    public int NutritionalInfoId { get; set; }

    public int? DishId { get; set; }

    public int? Calories { get; set; }

    public decimal? Protein { get; set; }

    public decimal? Carbs { get; set; }

    public decimal? Fat { get; set; }

    public virtual Dish? Dish { get; set; }
}
