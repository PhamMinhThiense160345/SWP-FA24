using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class MembershipTierView
{
    public int TierId { get; set; }

    public string? TierName { get; set; }

    public int? RequiredPoints { get; set; }

    public decimal? DiscountRate { get; set; }

}
