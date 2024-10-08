using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class Comment
{
    public int CommentId { get; set; }

    public int ArticleId { get; set; }

    public int UserId { get; set; }

    public string? Content { get; set; }

    public DateTime? PostDate { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual ICollection<CommentImage> CommentImages { get; set; } = new List<CommentImage>();

    public virtual User User { get; set; } = null!;
}
