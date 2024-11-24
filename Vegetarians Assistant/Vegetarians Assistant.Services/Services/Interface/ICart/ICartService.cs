using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.ICart
{
    public interface ICartService
    {
        Task addToCart(CartInfoView view);
        Task deleteFromCart(int cartId);
        Task<List<CartView?>> GetCartByUserId(int id);

        Task<bool> RemoveCartByUserId(int id);

        Task<bool> UpdateDishQuantityByCartId(int id, int newQuantity);

        Task<int?> AddPaymentAysnc(AddPaymentView payment);

        Task<bool> UpdatePaymentStatusAsync(int paymentId, string status);
        Task<(bool, string)> UpdatePaymentAysnc(PaymentView payment);
    }
}
