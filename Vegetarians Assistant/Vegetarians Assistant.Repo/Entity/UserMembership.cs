using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class UserMembership
{
    public int UserId { get; set; }

    public int? TierId { get; set; }

    public int? AccumulatedPoints { get; set; }

    public DateTime? DiscountGrantedDate { get; set; }

    public DateTime? LastDiscountUsed { get; set; }

    public virtual MembershipTier? Tier { get; set; }

    public virtual User User { get; set; } = null!;
}
