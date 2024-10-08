using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class CommentImage
{
    public int CommentImageId { get; set; }

    public int? CommentId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Comment? Comment { get; set; }
}
