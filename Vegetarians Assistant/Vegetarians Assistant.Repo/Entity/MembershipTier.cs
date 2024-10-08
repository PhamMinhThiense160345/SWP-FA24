using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class MembershipTier
{
    public int TierId { get; set; }

    public string? TierName { get; set; }

    public int? RequiredPoints { get; set; }

    public decimal? DiscountRate { get; set; }

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
