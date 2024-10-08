using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class HealthRecord
{
    public int RecordId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? RecordDate { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Height { get; set; }

    public string? Notes { get; set; }

    public virtual User? User { get; set; }
}
