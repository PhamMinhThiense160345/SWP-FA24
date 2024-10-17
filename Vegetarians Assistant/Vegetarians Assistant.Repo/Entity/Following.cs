using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Following
{
    public int FollowingId { get; set; }

    public int? UserId { get; set; }

    public int? FollowingUserId { get; set; }

    public DateTime? FollowDate { get; set; }

    public virtual User? FollowingUser { get; set; }

    public virtual User? User { get; set; }
}
