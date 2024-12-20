using Net.payOS;
using Net.payOS.Types;
using System.Globalization;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.API.Helpers.PayOs;

public class PayOSHelper : IPayOSHelper
{

    public async Task<string> CreatePaymentLink(int paymentId, List<OrderDetailInfo?> orderDetails, PayOSModel m, decimal shippingFee, decimal amount)
    {
        var items = new List<ItemData>();

        foreach (var details in orderDetails ?? new List<OrderDetailInfo?>())
        {
            items.Add(
                new ItemData(
                    details!.DishName!,
                    details.Quantity ?? 1,
                    (int)(details.Price ?? 1000)));
        }

        items.Add(new ItemData("Phí Giao Hàng", 1, (int)shippingFee));

        // Sử dụng biến amount thay cho total
        var payOS = new PayOS(m.ClientId, m.ApiKey, m.ChecksumKey);

        var paymentData = new PaymentData(
            paymentId,
            (int)amount, // Dùng amount được truyền vào
            $"Thanh toán đơn hàng #{paymentId}",
            items,
            cancelUrl: "https://vegetariansassistant-behjaxfhfkeqhbhk.southeastasia-01.azurewebsites.net/api/v1/carts/cancel?paymentId=" + paymentId,
            returnUrl: "https://vegetariansassistant-behjaxfhfkeqhbhk.southeastasia-01.azurewebsites.net/api/v1/carts/complete?paymentId=" + paymentId);

        var createPayment = await payOS.createPaymentLink(paymentData);
        return createPayment.checkoutUrl;
    }


}
