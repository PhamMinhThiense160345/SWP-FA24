using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class DeliveryView
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}
