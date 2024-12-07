using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class MenuView
    {
        public int MenuId { get; set; }

        public int? UserId { get; set; }

        public string MenuName { get; set; } = null!;

        public string? MenuDescription { get; set; }
    }
}
