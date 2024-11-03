using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Dish
{
    public int DishId { get; set; }

    public string Name { get; set; } = null!;

    public string? DishType { get; set; }

    public string? Description { get; set; }

    public string? Recipe { get; set; }

    public string? ImageUrl { get; set; }

    public int? DietaryPreferenceId { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual DietaryPreference? DietaryPreference { get; set; }

    public virtual ICollection<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();

    public virtual ICollection<FavoriteDish> FavoriteDishes { get; set; } = new List<FavoriteDish>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<FixedMenuItem> FixedMenuItems { get; set; } = new List<FixedMenuItem>();
}
