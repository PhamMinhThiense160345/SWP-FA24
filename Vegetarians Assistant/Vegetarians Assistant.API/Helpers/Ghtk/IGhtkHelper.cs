using Vegetarians_Assistant.API.Helpers.Ghtk.Models;
using Vegetarians_Assistant.API.Helpers.Ghtk.Models.Addresses;
using Vegetarians_Assistant.API.Helpers.Ghtk.Models.Orders;

namespace Vegetarians_Assistant.API.Helpers.Ghtk;

public interface IGhtkHelper
{
    Task<ResponseModel<long?>> CreateAsync(CreateGhtkOrderRequest model);
    Task<ResponseModel<OrderInfo?>> TrackAsync(long trackingId);
    Task<ResponseModel> CancelAsync(long trackingId);
    AddressInfo ExtractAddressParts(string address);
}
