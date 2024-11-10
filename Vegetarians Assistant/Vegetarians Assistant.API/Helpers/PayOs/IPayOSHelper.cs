using Vegetarians_Assistant.API.Requests;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.API.Helpers.PayOs;

public interface IPayOSHelper
{
    Task<string> CreatePaymentLink(int paymentId, List<OrderDetailInfo?> orderDetails, PayOSModel model);

}
