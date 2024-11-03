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
        public CartService( IMapper mapper, IUnitOfWork unitOfWork, CartRepository cartRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
        }

        public async Task addToCart(CartView view)
        {
            var cart = _mapper.Map<Cart>(view);
            var isCartExist = await _cartRepository.getCartByUserIdAndDishId(view.UserId, view.DishId);
            if (isCartExist == null)
            {
                await _unitOfWork.CartRepository.InsertAsync(cart);
            }
            else
            {
                isCartExist.Quantity = view.Quantity;
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

                foreach (var cart in carts)
                {
                    cartViews.Add(new CartView
                    {
                        UserId = cart.UserId,
                        DishId = cart.DishId,
                        Price = cart.DishId.HasValue && preferenceDictionary.ContainsKey(cart.DishId.Value)
                    ? preferenceDictionary[cart.DishId.Value]
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
        public Task deleteFromCart(int cartId)
        {
            throw new NotImplementedException();
        }
    }
}
