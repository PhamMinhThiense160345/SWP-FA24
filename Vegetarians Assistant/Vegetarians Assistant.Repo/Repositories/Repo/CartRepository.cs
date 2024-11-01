using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Implement;

namespace Vegetarians_Assistant.Repo.Repositories.Repo
{
    public class CartRepository : GenericRepository<Cart>
    {
        public CartRepository(VegetariansAssistantV3Context context) : base(context)
        {
        }

        public async Task<Cart?> getCartByUserIdAndDishId(int? userId, int? dishId) => await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.DishId == dishId);
    }
}
