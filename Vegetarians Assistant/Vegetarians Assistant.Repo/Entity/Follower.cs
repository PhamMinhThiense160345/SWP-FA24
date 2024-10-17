using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Follower
{
    public int FollowerId { get; set; }

    public int? UserId { get; set; }

    public int? FollowerUserId { get; set; }

    public DateTime? FollowDate { get; set; }

    public virtual User? FollowerUser { get; set; }

    public virtual User? User { get; set; }
}
