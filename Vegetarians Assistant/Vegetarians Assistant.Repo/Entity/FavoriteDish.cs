using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class FavoriteDish
{
    public int FavoriteId { get; set; }

    public int UserId { get; set; }

    public int DishId { get; set; }

    public DateTime? FavoriteDate { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
