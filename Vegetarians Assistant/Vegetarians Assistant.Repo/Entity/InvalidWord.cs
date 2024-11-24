using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class InvalidWord
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;
}
