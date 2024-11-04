using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.ICart
{
    public interface ICartService
    {
        Task addToCart(CartInfoView view);
        Task deleteFromCart(int cartId);
        Task<List<CartView?>> GetCartByUserId(int id);

        Task<bool> RemoveCartByUserId(int id);
    }
}
