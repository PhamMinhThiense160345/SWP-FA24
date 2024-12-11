using Newtonsoft.Json;

namespace Vegetarians_Assistant.API.Helpers.Ghtk.Models.Orders;

public class OrderInfo
{
    [JsonProperty("status")]
    public required int Status { get; set; }

    [JsonProperty("status_text")]
    public required string StatusText { get; set; }

    [JsonProperty("created")]
    public DateTime Created { get; set; }

    [JsonProperty("modified")]
    public DateTime Modified { get; set; }

    [JsonProperty("message")]
    public required string Message { get; set; }

    [JsonProperty("pick_date")]
    public DateTime PickDate { get; set; }

    [JsonProperty("deliver_date")]
    public DateTime DeliverDate { get; set; }

    [JsonProperty("customer_fullname")]
    public required string CustomerFullname { get; set; }

    [JsonProperty("customer_tel")]
    public required string CustomerTel { get; set; }

    [JsonProperty("address")]
    public required string Address { get; set; }

    [JsonProperty("ship_money")]
    public int ShipMoney { get; set; }

    [JsonProperty("insurance")]
    public int Insurance { get; set; }

    [JsonProperty("value")]
    public int Value { get; set; }

    [JsonProperty("pick_money")]
    public int PickMoney { get; set; }
}
