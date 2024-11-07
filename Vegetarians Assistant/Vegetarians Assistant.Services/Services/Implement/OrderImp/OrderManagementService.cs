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
using static System.Net.Mime.MediaTypeNames;

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
                newOrder.CompletedTime = null;
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

        public async Task<bool> CreateOrderDetail(OrderDetailView newOrder)
        {
            try
            {
                bool status = false;
                var order = _mapper.Map<OrderDetail>(newOrder);
                var insertedOrder = (await _unitOfWork.OrderDetailRepository.GetAsync(c => c.OrderId == newOrder.OrderId && c.DishId == newOrder.DishId)).FirstOrDefault();

                if (insertedOrder == null)
                {
                    await _unitOfWork.OrderDetailRepository.InsertAsync(order);
                    await _unitOfWork.SaveAsync();
                    status = true;
                }
                else
                {
                    insertedOrder.Price = newOrder.Price;
                    insertedOrder.Quantity = newOrder.Quantity;
                    await _unitOfWork.OrderDetailRepository.UpdateAsync(insertedOrder);
                    await _unitOfWork.SaveAsync();
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<OrderView?>> GetOrderByStatus(string Status)
        {

            try
            {
                var orders = await _unitOfWork.OrderRepository.FindAsync(c => c.Status == Status);
                var orderViews = new List<OrderView>();

                foreach (var order in orders)
                {
                    orderViews.Add(new OrderView
                    {
                        OrderId = order.OrderId,
                        Status = order.Status,
                        CompletedTime = order.CompletedTime,
                        DeliveryAddress = order.DeliveryAddress,
                        DeliveryFailedFee = order.DeliveryFailedFee,
                        DeliveryFee = order.DeliveryFee,
                        Note = order.Note,
                        OrderDate = order.OrderDate,
                        TotalPrice = order.TotalPrice,
                        UserId = order.UserId
                    });
                }
                return orderViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrderView?>> GetOrderByUserId(int id)
        {

            try
            {
                var orders = await _unitOfWork.OrderRepository.FindAsync(c => c.UserId == id);
                var orderViews = new List<OrderView>();

                foreach (var order in orders)
                {
                    orderViews.Add(new OrderView
                    {
                        OrderId = order.OrderId,
                        Status = order.Status,
                        CompletedTime = order.CompletedTime,
                        DeliveryAddress = order.DeliveryAddress,
                        DeliveryFailedFee = order.DeliveryFailedFee,
                        DeliveryFee = order.DeliveryFee,
                        Note = order.Note,
                        OrderDate = order.OrderDate,
                        TotalPrice = order.TotalPrice,
                        UserId = order.UserId
                    });
                }
                return orderViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrderDetailInfo?>> GetOrderDetailOrderId(int id)
        {

            try
            {
                var orders = await _unitOfWork.OrderDetailRepository.FindAsync(c => c.OrderId == id);
                var orderViews = new List<OrderDetailInfo>();
                var dishIds = new HashSet<int>();
                foreach (var order in orders)
                {
                    if (order.DishId.HasValue)
                    {
                        dishIds.Add(order.DishId.Value);
                    }
                }
                var dishs = await _unitOfWork.DishRepository.GetAsync(dp => dishIds.Contains(dp.DishId));

                var preferenceDictionary = new Dictionary<int, string>();
                foreach (var preference in dishs)
                {
                    preferenceDictionary[preference.DishId] = preference.Name;
                }
                foreach (var order in orders)
                {
                    orderViews.Add(new OrderDetailInfo
                    {
                        OrderDetailId = order.OrderDetailId,
                        DishId = order.DishId,
                        DishName = order.DishId.HasValue && preferenceDictionary.ContainsKey(order.DishId.Value)
                    ? preferenceDictionary[order.DishId.Value]
                    : null,
                        OrderId = order.OrderId,
                        Price = order.Price,
                        Quantity = order.Quantity
                    });
                }
                return orderViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrderView>> GetAllOrder()
        {
            try
            {
                var orders = (await _unitOfWork.OrderRepository.GetAsync()).ToList();
                List<OrderView> orderViews = new List<OrderView>();

                foreach (var order in orders)
                {
                    var orderView = new OrderView()
                    {
                        OrderId = order.OrderId,
                        Status = order.Status,
                        CompletedTime = order.CompletedTime,
                        DeliveryAddress = order.DeliveryAddress,
                        DeliveryFailedFee = order.DeliveryFailedFee,
                        DeliveryFee = order.DeliveryFee,
                        Note = order.Note,
                        OrderDate = order.OrderDate,
                        TotalPrice = order.TotalPrice,
                        UserId = order.UserId
                    };
                    orderViews.Add(orderView);
                }
                return orderViews;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ChangeOrderStatus(int id, string newStatus)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIDAsync(id);

                if (order == null)
                {
                    return false; // Order not found
                }

                order.Status = newStatus;
                await _unitOfWork.OrderRepository.UpdateAsync(order);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ChangeOrderDeliveryFailedFee(int orderId, decimal newDeliveryFailedFee)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIDAsync(orderId);

                if (order == null)
                {
                    return false;
                }

                order.DeliveryFailedFee = newDeliveryFailedFee;
                await _unitOfWork.OrderRepository.UpdateAsync(order);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
