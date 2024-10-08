using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class ArticleImage
{
    public int ArticleImageId { get; set; }

    public int? ArticleId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Article? Article { get; set; }
}
