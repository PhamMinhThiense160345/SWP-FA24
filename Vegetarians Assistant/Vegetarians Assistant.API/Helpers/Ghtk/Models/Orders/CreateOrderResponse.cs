using Newtonsoft.Json;

namespace Vegetarians_Assistant.API.Helpers.Ghtk.Models.Orders;

public class CreateOrderResponse
{
    [JsonProperty("tracking_id")]
    public long TrackingId { get; set; }
}

