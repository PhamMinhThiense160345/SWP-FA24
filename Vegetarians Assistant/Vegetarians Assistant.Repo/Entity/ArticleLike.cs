using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class ArticleLike
{
    public int LikeId { get; set; }

    public int? ArticleId { get; set; }

    public int? UserId { get; set; }

    public DateTime? LikeDate { get; set; }

    public virtual Article? Article { get; set; }

    public virtual User? User { get; set; }
}
