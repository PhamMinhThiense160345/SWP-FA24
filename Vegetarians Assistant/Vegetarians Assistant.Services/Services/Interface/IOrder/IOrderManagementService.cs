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
        Task<bool> CreateOrderDetail(OrderDetailView newOrder);
        Task<List<OrderView?>> GetOrderByStatus(string Status);
        Task<List<OrderView?>> GetOrderByUserId(int id);
        Task<List<PaymentView?>> GetPaymentDetailByOrderId(int id);
        Task<List<OrderDetailInfo?>> GetOrderDetailOrderId(int id);
        Task<List<OrderView>> GetAllOrder();
        Task<bool> ChangeOrderStatus(int orderId, string newStatus);
        Task<bool> ChangeOrderDeliveryFailedFee(int orderId, decimal newDeliveryFailedFee);
        Task<OrderView?> GetOrderById(int id);
        Task<bool> UpdateDeliveryFee(int id, decimal newDeliveryFee);

    }
}
