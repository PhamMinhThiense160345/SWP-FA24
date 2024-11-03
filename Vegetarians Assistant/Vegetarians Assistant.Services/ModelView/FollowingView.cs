using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class FollowingView
    {
        public int FollowingId { get; set; }

        public int? UserId { get; set; }

        public int? FollowingUserId { get; set; }

        public DateTime? FollowDate { get; set; }
    }
}
