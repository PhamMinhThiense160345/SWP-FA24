using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IShipping;

namespace Vegetarians_Assistant.Services.Services.Implement.ShippingImp
{
    public class ShippingManagementService : IShippingManagementService
    {
        private readonly IGenericRepository<Shipping> _shippingRepository;
        private readonly IMapper _mapper;

        public ShippingManagementService(IGenericRepository<Shipping> shippingRepository, IMapper mapper)
        {
            _shippingRepository = shippingRepository;
            _mapper = mapper;
        }

        // Triển khai phương thức CreateShipping
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

        // Triển khai phương thức UpdateShippingByOrderId
        public async Task<bool> UpdateShippingByOrderId(int orderId, ShippingView updatedShipping)
        {
            try
            {
                var shipping = await _shippingRepository.FindAsync(s => s.OrderId == orderId);
                var shippingToUpdate = shipping.FirstOrDefault();

                if (shippingToUpdate == null)
                {
                    return false;
                }

                shippingToUpdate.ShipperName = updatedShipping.ShipperName;
                shippingToUpdate.PhoneNumber = updatedShipping.PhoneNumber;
                shippingToUpdate.TransportCompany = updatedShipping.TransportCompany;
                shippingToUpdate.PickupTime = updatedShipping.PickupTime;
                shippingToUpdate.DeliveryTime = updatedShipping.DeliveryTime;
                shippingToUpdate.FailureReason = updatedShipping.FailureReason;

                await _shippingRepository.UpdateAsync(shippingToUpdate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Triển khai phương thức GetShippingByOrderId
        public async Task<ShippingView> GetShippingByOrderId(int orderId)
        {
            try
            {
                var shipping = await _shippingRepository.FindAsync(s => s.OrderId == orderId);
                var shippingToReturn = shipping.FirstOrDefault();

                if (shippingToReturn == null)
                {
                    return null;  // Nếu không tìm thấy, trả về null
                }

                return _mapper.Map<ShippingView>(shippingToReturn);
            }
            catch (Exception)
            {
                return null;  // Nếu có lỗi, trả về null
            }
        }

        // Triển khai phương thức GetAllShippings
        public async Task<IEnumerable<ShippingView>> GetAllShippings()
        {
            try
            {
                var allShippings = await _shippingRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ShippingView>>(allShippings);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ShippingView>();  // Nếu có lỗi, trả về một danh sách rỗng
            }
        }
    }
}
