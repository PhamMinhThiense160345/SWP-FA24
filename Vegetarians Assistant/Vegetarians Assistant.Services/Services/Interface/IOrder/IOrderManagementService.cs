using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.IOrder
{
    public interface IOrderManagementService
    {
        Task<bool> CreateOrderByCustomer(OrderView newOrder);
        Task<List<OrderView?>> GetOrderByStatus(string Status);
        Task<List<OrderView?>> GetOrderByUserId(int id);
        Task<List<OrderView>> GetAllOrder();
        Task<bool> ChangeOrderStatus(int orderId, string newStatus);
        Task<bool> ChangeOrderDeliveryFailedFee(int orderId, decimal newDeliveryFailedFee);

    }
}
