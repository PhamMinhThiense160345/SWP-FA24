using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class TotalNutritionDish
{
    public int DishId { get; set; }

    public string DishName { get; set; } = null!;

    public decimal? TotalWeight { get; set; }

    public decimal? Calories { get; set; }

    public decimal? Protein { get; set; }

    public decimal? Carbs { get; set; }

    public decimal? Fat { get; set; }

    public decimal? Fiber { get; set; }

    public decimal? VitaminA { get; set; }

    public decimal? VitaminB { get; set; }

    public decimal? VitaminC { get; set; }

    public decimal? VitaminD { get; set; }

    public decimal? VitaminE { get; set; }

    public decimal? Calcium { get; set; }

    public decimal? Iron { get; set; }

    public decimal? Magnesium { get; set; }

    public decimal? Omega3 { get; set; }

    public decimal? Sugars { get; set; }

    public decimal? Cholesterol { get; set; }

    public decimal? Sodium { get; set; }
}
