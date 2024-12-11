using AutoMapper;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.Services.Interface.IShipping;

namespace Vegetarians_Assistant.Services.Services.Implement.ShippingImp;
public class ShippingManagementService(IGenericRepository<Shipping> shippingRepository, IMapper mapper) : IShippingManagementService
{
    private readonly IGenericRepository<Shipping> _shippingRepository = shippingRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> CreateShipping(ShippingView newShipping)
    {
        try
        {
            var shippingEntity = _mapper.Map<Shipping>(newShipping);
            await _shippingRepository.InsertAsync(shippingEntity);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateShippingByTrackingId(long trackingId, ShippingView updatedShipping)
    {
        try
        {
            var shipping = await _shippingRepository.FindAsync(s => s.TrackingId == trackingId);
            var shippingToUpdate = shipping.FirstOrDefault();

            if (shippingToUpdate == null) return false;

            shippingToUpdate.Status = updatedShipping.Status;
            shippingToUpdate.StatusText = updatedShipping.StatusText;
            shippingToUpdate.Created = updatedShipping.Created;
            shippingToUpdate.Modified = updatedShipping.Modified;
            shippingToUpdate.Message = updatedShipping.Message;
            shippingToUpdate.PickDate = updatedShipping.PickDate;
            shippingToUpdate.DeliverDate = updatedShipping.DeliverDate;
            shippingToUpdate.CustomerFullname = updatedShipping.CustomerFullname;
            shippingToUpdate.CustomerTel = updatedShipping.CustomerTel;
            shippingToUpdate.Address = updatedShipping.Address;
            shippingToUpdate.ShipMoney = updatedShipping.ShipMoney;
            shippingToUpdate.Insurance = updatedShipping.Insurance;
            shippingToUpdate.Value = updatedShipping.Value;
            shippingToUpdate.PickMoney = updatedShipping.PickMoney;

            await _shippingRepository.UpdateAsync(shippingToUpdate);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<ShippingView?> GetShippingByTrackingId(long trackingId)
    {
        try
        {
            var shipping = await _shippingRepository.FindAsync(s => s.TrackingId == trackingId);
            var shippingToReturn = shipping.FirstOrDefault();

            if (shippingToReturn == null) return null;
            return _mapper.Map<ShippingView>(shippingToReturn);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<ShippingView>> GetAllShippings()
    {
        try
        {
            var allShippings = await _shippingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ShippingView>>(allShippings);
        }
        catch (Exception)
        {
            return Enumerable.Empty<ShippingView>();
        }
    }
}
