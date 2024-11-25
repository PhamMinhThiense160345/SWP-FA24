using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class ArticleBody
{
    public int BodyId { get; set; }

    public int ArticleId { get; set; }

    public string? Content { get; set; }

    public string? ImageUrl { get; set; }

    public int Position { get; set; }

    public int UserId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
