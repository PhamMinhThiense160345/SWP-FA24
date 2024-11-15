using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.ICart;


namespace Vegetarians_Assistant.Services.Services.Interface.CartImp
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CartRepository _cartRepository;
        public CartService(IMapper mapper, IUnitOfWork unitOfWork, CartRepository cartRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
        }

        public async Task addToCart(CartInfoView view)
        {
            var cart = _mapper.Map<Cart>(view);
            var isCartExist = await _cartRepository.getCartByUserIdAndDishId(view.UserId, view.DishId);
            if (isCartExist == null)
            {
                await _unitOfWork.CartRepository.InsertAsync(cart);
            }
            else
            {
                isCartExist.Quantity += view.Quantity;
                await _unitOfWork.CartRepository.UpdateAsync(isCartExist);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<List<CartView?>> GetCartByUserId(int id)
        {

            try
            {
                var carts = await _unitOfWork.CartRepository.FindAsync(c => c.UserId == id);
                var cartViews = new List<CartView>();

                var dishIds = new HashSet<int>();
                foreach (var cart in carts)
                {
                    if (cart.DishId.HasValue)
                    {
                        dishIds.Add(cart.DishId.Value);
                    }
                }

                var dishes = await _unitOfWork.DishRepository.GetAsync(dp => dishIds.Contains(dp.DishId));

                var preferenceDictionary = new Dictionary<int, decimal>();
                foreach (var preference in dishes)
                {
                    preferenceDictionary[preference.DishId] = (decimal)preference.Price;
                }

                var preferenceDictionary2 = new Dictionary<int, string>();
                foreach (var preferences in dishes)
                {
                    preferenceDictionary2[preferences.DishId] = preferences.Name;
                }


                foreach (var cart in carts)
                {
                    cartViews.Add(new CartView
                    {
                        UserId = cart.UserId,
                        DishId = cart.DishId,
                        Price = cart.DishId.HasValue && preferenceDictionary.ContainsKey(cart.DishId.Value)
                    ? preferenceDictionary[cart.DishId.Value]
                    : null,
                        DishName = cart.DishId.HasValue && preferenceDictionary2.ContainsKey(cart.DishId.Value)
                    ? preferenceDictionary2[cart.DishId.Value]
                    : null,
                        CartId = cart.CartId,
                        Quantity = cart.Quantity,
                        TotalPrice = (decimal)((cart.Quantity) * (cart.DishId.HasValue && preferenceDictionary.ContainsKey(cart.DishId.Value)
                    ? preferenceDictionary[cart.DishId.Value]
                    : null))
                    });
                }
                return cartViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveCartByUserId(int id)
        {
            try
            {
                var cartes = (await _unitOfWork.CartRepository.GetByIDAsync(id));

                if (cartes != null)
                {
                    await _unitOfWork.CartRepository.DeleteAsync(cartes);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateDishQuantityByCartId(int id, int newQuantity)
        {
            try
            {
                var cart = await _unitOfWork.CartRepository.GetByIDAsync(id);

                if (cart == null)
                {
                    return false;
                }

                cart.Quantity = newQuantity;
                await _unitOfWork.CartRepository.UpdateAsync(cart);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task deleteFromCart(int cartId)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> AddPaymentAysnc(AddPaymentView payment)
        {
            try
            {
                var newPayment = new PaymentDetail()
                {
                    PaymentId = payment.PaymentId,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    PaymentMethod = payment.PaymentMethod,
                    PaymentStatus = payment.PaymentStatus,
                    PaymentDate = payment.PaymentDate,
                    RefundAmount = payment.RefundAmount,
                    ReturnUrl = payment.ReturnUrl,
                    CancelUrl = payment.CancelUrl,
                    TransactionId = payment.TransactionId,
                };

                await _unitOfWork.PaymentDetailRepository.InsertAsync(newPayment);
                await _unitOfWork.SaveAsync();

                return newPayment.PaymentId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePaymentStatusAsync(int paymentId, string status)
        {
            try
            {
                var payment = await _unitOfWork.PaymentDetailRepository.GetByIDAsync(paymentId);

                if (payment is null) return false;
                payment.PaymentStatus = status;

                await _unitOfWork.PaymentDetailRepository.UpdateAsync(payment);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
