using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Menu
{
    public int MenuId { get; set; }

    public int? UserId { get; set; }

    public string? MenuName { get; set; }

    public string? MenuDescription { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<MenuDish> MenuDishes { get; set; } = new List<MenuDish>();

    public virtual User? User { get; set; }
}
