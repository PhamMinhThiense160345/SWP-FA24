using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? DishId { get; set; }

    public int? UserId { get; set; }

    public int OrderId { get; set; }

    public decimal? Rating { get; set; }

    public string? FeedbackContent { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
