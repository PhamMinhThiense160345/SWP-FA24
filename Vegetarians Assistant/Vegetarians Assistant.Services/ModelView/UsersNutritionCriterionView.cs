using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class UsersNutritionCriterionView
    {
        public int UserNutritionCriteriaId { get; set; }

        public int? UserId { get; set; }

        public int? CriteriaId { get; set; }
    }
}
