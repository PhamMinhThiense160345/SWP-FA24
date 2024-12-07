using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class MenuInfoView
    {
        public int MenuId { get; set; }

        public int? UserId { get; set; }

        public string? MenuName { get; set; }

        public string? MenuDescription { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
