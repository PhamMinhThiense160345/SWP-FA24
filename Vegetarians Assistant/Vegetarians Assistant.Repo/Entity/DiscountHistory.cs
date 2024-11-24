using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class DiscountHistory
{
    public int DiscountHistoryId { get; set; }

    public int? UserId { get; set; }

    public int? TierId { get; set; }

    public DateTime? GrantedDate { get; set; }

    public decimal? DiscountRate { get; set; }

    public string? Status { get; set; }

    public DateTime? ExpirationDate { get; set; }
}
