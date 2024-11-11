using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class UserView
    {
        public int UserId { get; set; }
        public string? Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public string? Profession { get; set; }

        public int? DietaryPreferenceId { get; set; }

        public string? Goal { get; set; }

        public string? Status { get; set; }

        public int? RoleId { get; set; }

        public string? RoleName { get; set; } = null!;

        public string? ActivityLevel { get; set; }
        public bool? IsPhoneVerified { get; set; }

    }
}
