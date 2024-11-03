using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IOrder;

namespace Vegetarians_Assistant.Services.Services.Implement.OrderImp
{
    public class OrderManagementService : IOrderManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrderByCustomer(OrderView newOrder)
        {
            try
            {
                bool status = false;
                newOrder.Status = "pending";
                newOrder.DeliveryFee = 0;
                newOrder.DeliveryFailedFee = 0;
                var order = _mapper.Map<Order>(newOrder);
                await _unitOfWork.OrderRepository.InsertAsync(order);
                await _unitOfWork.SaveAsync();
                var insertedOrder = await _unitOfWork.OrderRepository.GetByIDAsync(order.OrderId);

                if (insertedOrder != null)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                var insertedOrder = (await _unitOfWork.OrderRepository.FindAsync(a => a.OrderId == newOrder.OrderId)).FirstOrDefault();
                if (insertedOrder != null)
                {
                    await _unitOfWork.OrderRepository.DeleteAsync(insertedOrder);
                    await _unitOfWork.SaveAsync();
                }
                throw new Exception(ex.Message);
            }
        }
    }
}
