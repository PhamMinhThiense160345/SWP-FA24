using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class FixedMenu
{
    public int FixedMenuId { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<FixedMenuItem> FixedMenuItems { get; set; } = new List<FixedMenuItem>();
}
