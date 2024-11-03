using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetarians_Assistant.Services.ModelView
{
    public class OrderView
    {
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        public decimal? TotalPrice { get; set; }

        public string? DeliveryAddress { get; set; }

        public string? Note { get; set; }

        public decimal? DeliveryFee { get; set; }

        public decimal? DeliveryFailedFee { get; set; }

        public DateTime? CompletedTime { get; set; }

        public DateTime? OrderDate { get; set; }
        
        public string? Status { get; set; }
    }
}
