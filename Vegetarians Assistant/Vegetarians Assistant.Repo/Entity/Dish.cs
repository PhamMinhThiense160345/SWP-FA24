using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Dish
{
    public int DishId { get; set; }

    public string Name { get; set; } = null!;

    public string? DishType { get; set; }

    public string? Description { get; set; }

    public string? Ingredients { get; set; }

    public string? Recipe { get; set; }

    public string? ImageUrl { get; set; }

    public int? DietaryPreferenceId { get; set; }

    public decimal? Price { get; set; }

    public virtual DietaryPreference? DietaryPreference { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<FixedMenuItem> FixedMenuItems { get; set; } = new List<FixedMenuItem>();

    public virtual ICollection<NutritionalInfo> NutritionalInfos { get; set; } = new List<NutritionalInfo>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
