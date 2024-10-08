using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Restaurant
{
    public int RestaurantId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? ActivityTime { get; set; }

    public string? Type { get; set; }

    public string? ContactNumber { get; set; }

    public string? Description { get; set; }

    public string? Website { get; set; }
}
