using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Vegetarians_Assistant.API.Helpers.AesEncryption;
using Vegetarians_Assistant.API.Helpers.PayOs;
using Vegetarians_Assistant.API.Requests;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Customer;
using Vegetarians_Assistant.Services.Services.Interface.ICart;
using Vegetarians_Assistant.Services.Services.Interface.IOrder;
using Vegetarians_Assistant.Services.Services.Interface.Membership;

namespace Vegetarians_Assistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(
    ICartService cartService,
    IEncryptionHelper encryptionHelper,
    IOrderManagementService orderManagementService,
    IPayOSHelper payOSHelper,
    IConfiguration config) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;
        private readonly IEncryptionHelper _encryptionHelper = encryptionHelper;
        private readonly IPayOSHelper _payOSHelper = payOSHelper;
        private readonly IConfiguration _config = config;
        private readonly IOrderManagementService _orderManagementService = orderManagementService;

        [HttpGet("/api/v1/carts/getCartByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<CartView>>> GetCartByUserId(int id)
        {
            var cartDetail = await _cartService.GetCartByUserId(id);
            if (cartDetail == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(cartDetail);
        }

        [HttpPost("/api/v1/carts/addToCart")]
        public async Task<IActionResult> addToCart([FromBody] CartInfoView view)
        {
            try
            {
                await _cartService.addToCart(view);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest("Thêm dish vào giỏ hàng thất bại");
            }
        }

        [HttpPut("/api/v1/carts/updateDishQuantityByCartId/{id}")]
        public async Task<IActionResult> UpdateDishQuantityByCartId(int id, [FromBody] int newQuantity)
        {
            try
            {
                var success = await _cartService.UpdateDishQuantityByCartId(id, newQuantity);

                if (success)
                {
                    return Ok("Cart quantity updated successfully");
                }
                else
                {
                    return NotFound("Cart not found or failed to update quantity");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/v1/carts/removeCartByUserId/{id}")]
        public async Task<IActionResult> RemoveCartByUserId(int id)
        {
            var success = await _cartService.RemoveCartByUserId(id);

            if (success)
            {
                return Ok("Cart deleted successfully");
            }
            else
            {
                return NotFound("Cart not found or failed to delete");
            }
        }



        [HttpPost("/api/v1/carts/checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            try
            {
                var payOSModel = GetPayOSModel(request.DecryptionKey);
                var orderDetail = await _orderManagementService.GetOrderDetailOrderId((int)request.OrderId);
                if (orderDetail.Count == 0) throw new Exception($"Order {request.OrderId} not exist");

                var payment = new AddPaymentView()
                {
                    OrderId = request.OrderId,
                    PaymentMethod = "PayOS",
                    PaymentStatus = "pending",
                    PaymentDate = DateTime.Now,
                    Amount = orderDetail.Sum(x => x!.Price * x.Quantity),
                    CancelUrl = "https://localhost:7157/api/v1/carts/cancel?orderId=" + request.OrderId,
                    ReturnUrl = "https://localhost:7157/api/v1/carts/complete?orderId=" + request.OrderId,
                };

                var paymentId = await _cartService.AddPaymentAysnc(payment);

                if (paymentId is null) throw new Exception("Add payment failed");

                var paymentLink = await _payOSHelper.CreatePaymentLink(paymentId ?? 0, orderDetail, payOSModel);
                return Ok(paymentLink);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      


        private PayOSModel GetPayOSModel(string key)
        {
            var clientIdEncrypted = _config.GetValue<string>("PayOS:ClientId");
            var apiKeyEncrypted = _config.GetValue<string>("PayOS:ApiKey");
            var checkSumKeyEncrypted = _config.GetValue<string>("PayOS:ChecksumKey");

            string a = _encryptionHelper.Decrypt(clientIdEncrypted!, key);
            string b = _encryptionHelper.Decrypt(apiKeyEncrypted!, key);
            string c = _encryptionHelper.Decrypt(checkSumKeyEncrypted!, key);

            return new PayOSModel(a, b, c);
        }


    }
}