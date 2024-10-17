using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public int? DishId { get; set; }

    public int? Quantity { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual Order? Order { get; set; }
}
