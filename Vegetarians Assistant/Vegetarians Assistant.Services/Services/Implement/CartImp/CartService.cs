using AutoMapper;
using System;
using System.Collections.Generic;
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


        public Task deleteFromCart(int cartId)
        {
            throw new NotImplementedException();
        }
    }
}
