using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class StaffView
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Status { get; set; }

        public int? RoleId { get; set; }
    }
}
