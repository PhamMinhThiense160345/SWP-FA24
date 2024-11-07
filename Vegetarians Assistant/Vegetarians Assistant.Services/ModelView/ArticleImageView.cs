using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class ArticleImageView
    {
        public int ArticleImageId { get; set; }

        public int? ArticleId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
