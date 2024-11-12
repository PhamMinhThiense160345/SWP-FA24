using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class ArticleLikeView
    {
        public int LikeId { get; set; }

        public int? ArticleId { get; set; }

        public int? UserId { get; set; }

        public DateTime? LikeDate { get; set; }
    }
}
