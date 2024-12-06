using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class ArticleInfoView
    {
        public int ArticleId { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Status { get; set; }

        public int? AuthorId { get; set; }

        //public DateOnly? ModerateDate { get; set; }
    }
}
