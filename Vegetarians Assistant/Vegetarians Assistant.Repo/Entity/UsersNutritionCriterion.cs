using System;
using System.Collections.Generic;

namespace Vegetarians_Assistant.Repo.Entity;

public partial class UsersNutritionCriterion
{
    public int UserNutritionCriteriaId { get; set; }

    public int? UserId { get; set; }

    public int? CriteriaId { get; set; }

    public virtual NutritionCriterion? Criteria { get; set; }

    public virtual User? User { get; set; }
}
