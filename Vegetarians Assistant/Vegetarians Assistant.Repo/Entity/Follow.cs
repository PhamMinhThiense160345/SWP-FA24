﻿using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Follow
{
    public int FollowId { get; set; }

    public int? FollowerId { get; set; }

    public int? FollowedId { get; set; }

    public DateTime? FollowDate { get; set; }

    public virtual User? Followed { get; set; }

    public virtual User? Follower { get; set; }
}
