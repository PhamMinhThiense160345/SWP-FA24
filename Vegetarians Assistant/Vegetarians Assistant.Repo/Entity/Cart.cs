using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Cart
{
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public int? DishId { get; set; }

    public int? Quantity { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual User? User { get; set; }
}
