using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class DietaryPreference
{
    public int Id { get; set; }

    public string PreferenceName { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
