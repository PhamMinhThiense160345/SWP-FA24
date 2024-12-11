using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.Services.Interface.IShipping;
public interface IShippingManagementService
{
    Task<bool> CreateShipping(ShippingView newShipping);
    Task<bool> UpdateShippingByTrackingId(long trackingId, ShippingView updatedShipping);
    Task<ShippingView?> GetShippingByTrackingId(long trackingId);
    Task<IEnumerable<ShippingView>> GetAllShippings();
}
