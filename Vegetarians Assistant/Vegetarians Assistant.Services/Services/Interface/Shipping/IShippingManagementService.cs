using System.Collections.Generic;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.IShipping

{
    public interface IShippingManagementService
    {
        Task<bool> CreateShipping(ShippingView newShipping);
        Task<bool> UpdateShippingByOrderId(int orderId, ShippingView updatedShipping);
        Task<ShippingView> GetShippingByOrderId(int orderId);  // Phương thức cần triển khai
        Task<IEnumerable<ShippingView>> GetAllShippings();
    }

}
