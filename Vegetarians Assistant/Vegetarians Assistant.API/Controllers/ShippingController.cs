using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.API.Helpers.Ghtk;
using Vegetarians_Assistant.API.Helpers.Ghtk.Models;
using Vegetarians_Assistant.API.Helpers.Ghtk.Models.Addresses;
using Vegetarians_Assistant.API.Helpers.Ghtk.Models.Orders;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Implement.ShippingImp;
using Vegetarians_Assistant.Services.Services.Interface.Admin;
using Vegetarians_Assistant.Services.Services.Interface.Dish;
using Vegetarians_Assistant.Services.Services.Interface.IOrder;
using Vegetarians_Assistant.Services.Services.Interface.IShipping;

namespace Vegetarians_Assistant.API.Controllers;

[Route("api/v1/shipping")]
[ApiController]
public class ShippingController(
    IGhtkHelper ghtkHelper,
    IOrderManagementService orderManagementService,
    IUserManagementService userManagementService,
    IDishManagementService dishManagementService,
    IShippingManagementService shippingManagementService) : ControllerBase
{
    private readonly IGhtkHelper _ghtkHelper = ghtkHelper;
    private readonly IOrderManagementService _orderManagementService = orderManagementService;
    private readonly IUserManagementService _userManagementService = userManagementService;
    private readonly IDishManagementService _dishManagementService = dishManagementService;
    private readonly IShippingManagementService _shippingManagementService = shippingManagementService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateShippingAsync([FromBody] CreateShippingRequest request)
    {
        try
        {
            var order = await _orderManagementService.GetOrderById(request.OrderId);
            var orderDetail = await _orderManagementService.GetOrderDetailOrderId(request.OrderId);

            if (orderDetail.Count == 0 || orderDetail == null || order == null || order.UserId == null)
                return BadRequest($"Order {request.OrderId} does not exist");

            var user = await _userManagementService.GetUserByUserId(order.UserId ?? 0);
            if (user is null) return BadRequest($"User not found");

            var deliveryAddress = _ghtkHelper.ExtractAddressParts(order.DeliveryAddress ?? string.Empty);

            var createOrderRequest = new CreateGhtkOrderRequest()
            {
                Order = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    PickName = PickInfo.Name ?? string.Empty,
                    PickAddress = PickInfo.Address ?? string.Empty,
                    PickProvince = PickInfo.Province,
                    PickDistrict = PickInfo.District,
                    PickTel = PickInfo.Tel ?? string.Empty,
                    PickMoney = PickInfo.PickMoney,
                    Hamlet = PickInfo.Hamlet,
                    Tel = user.PhoneNumber ?? string.Empty,
                    Name = user.Username ?? string.Empty,
                    Address = deliveryAddress.Address ?? string.Empty,
                    Province = deliveryAddress?.Province ?? string.Empty,
                    District = deliveryAddress?.District ?? string.Empty,
                    Ward = deliveryAddress?.Ward ?? string.Empty,
                    Value = (int)(order.TotalPrice ?? 0),
                },
                Products = await MapToItem(orderDetail)
            };

            var createOrderResponse = await _ghtkHelper.CreateAsync(createOrderRequest);
            var trackingId = createOrderResponse.Content ?? 0;

            var trackOrderResponse = await _ghtkHelper.TrackAsync(trackingId);
            var orderInfo = trackOrderResponse.Content;
            if (orderInfo is not null)
            {
                var newShipping = new ShippingView()
                {
                    TrackingId = trackingId,
                    OrderId = order.OrderId,
                    UserId = user.UserId,
                    Status = orderInfo.Status,
                    StatusText = orderInfo.StatusText,
                    Created = orderInfo.Created,
                    Modified = orderInfo.Modified,
                    Message = orderInfo.Message,
                    PickDate = orderInfo.PickDate,
                    DeliverDate = orderInfo.DeliverDate,
                    CustomerFullname = orderInfo.CustomerFullname,
                    CustomerTel = orderInfo.CustomerTel,
                    Address = orderInfo.Address,
                    ShipMoney = orderInfo.ShipMoney,
                    Insurance = orderInfo.Insurance,
                    Value = orderInfo.Value,
                    PickMoney = orderInfo.PickMoney,
                };

                await _shippingManagementService.CreateShipping(newShipping);
            }
            return Ok(createOrderResponse);
        }
        catch (Exception ex)
        {
            return Ok(new ResponseModel(false, ex.Message));
        }
    }

    

    #region private
    private async Task<List<Product>> MapToItem(List<OrderDetailInfo?> items)
    {
        var list = new List<Product>();
        foreach (var item in items)
        {
            var dish = await _dishManagementService.GetDishByDishId(item?.DishId ?? 0);

            if (dish is not null)
            {
                var listItem = new Product()
                {
                    Name = dish.Name,
                    Price = (double)(dish.Price ?? 0),
                    Quantity = item?.Quantity ?? 0,
                    Weight = 1,
                };
                list.Add(listItem);
            }
        }
        return list;

    }
    #endregion
}
