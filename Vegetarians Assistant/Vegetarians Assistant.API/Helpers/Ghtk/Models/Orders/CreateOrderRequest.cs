using Newtonsoft.Json;

namespace Vegetarians_Assistant.API.Helpers.Ghtk.Models.Orders;

public record CreateShippingRequest(int OrderId);

public class CreateGhtkOrderRequest
{
    [JsonProperty("products")]
    public required List<Product> Products { get; set; }

    [JsonProperty("order")]
    public required Order Order { get; set; }
}

public class Product
{
    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("weight")]
    public double Weight { get; set; }

    [JsonProperty("price")]
    public double Price { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}

public class Order
{
    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("pick_name")]
    public required string PickName { get; set; }

    [JsonProperty("pick_address")]
    public required string PickAddress { get; set; }

    [JsonProperty("pick_province")]
    public required string PickProvince { get; set; }

    [JsonProperty("pick_district")]
    public required string PickDistrict { get; set; }

    [JsonProperty("pick_tel")]
    public required string PickTel { get; set; }

    [JsonProperty("tel")]
    public required string Tel { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("address")]
    public required string Address { get; set; }

    [JsonProperty("province")]
    public required string Province { get; set; }

    [JsonProperty("district")]
    public required string District { get; set; }

    [JsonProperty("ward")]
    public required string Ward { get; set; }

    [JsonProperty("hamlet")]
    public required string Hamlet { get; set; }

    [JsonProperty("pick_money")]
    public int PickMoney { get; set; }

    [JsonProperty("value")]
    public int Value { get; set; }
}

