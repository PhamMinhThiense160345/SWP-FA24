using Newtonsoft.Json;
using System.Text;
using Vegetarians_Assistant.API.Helpers.Ghtk.Models;
using Vegetarians_Assistant.API.Helpers.Ghtk.Models.Addresses;
using Vegetarians_Assistant.API.Helpers.Ghtk.Models.Orders;

namespace Vegetarians_Assistant.API.Helpers.Ghtk;

public class GhtkHelper : IGhtkHelper
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public GhtkHelper(IConfiguration configuration, HttpClient client)
    {
        var token = configuration["Ghtk:Credentials:Token"];
        var clientSource = configuration["Ghtk:Credentials:X-Client-Source"];

        client.DefaultRequestHeaders.Add("Token", token);
        client.DefaultRequestHeaders.Add("X-Client-Source", clientSource);

        _client = client;
        _configuration = configuration;
    }

    public async Task<ResponseModel<long?>> CreateAsync(CreateGhtkOrderRequest model)
    {
        var url = _configuration["Ghtk:Api:Order:Create"];
        var json = JsonConvert.SerializeObject(model);

        var response = await PostAsync(url, json);
        var success = Convert.ToBoolean(response?.success);
        var message = Convert.ToString(response?.message);
        long? trackingId = response?.order != null ? Convert.ToInt64(response?.order?.tracking_id) : null;

        return new(success, message, trackingId);
    }

    public async Task<ResponseModel<OrderInfo?>> TrackAsync(long trackingId)
    {
        var url = _configuration["Ghtk:Api:Order:Track"];
        var response = await GetAsync(url + "/" + trackingId);
        
        var success = Convert.ToBoolean(response?.success);
        var message = Convert.ToString(response?.message);
        var jsonResponse = Convert.ToString(response?.order);
        var order = jsonResponse == null ? null : 
            JsonConvert.DeserializeObject<OrderInfo?>(jsonResponse);

        return new(success, message, order);
    }

    public async Task<ResponseModel> CancelAsync(long trackingId)
    {
        var url = _configuration["Ghtk:Api:Order:Cancel"];
        var response = await PostAsync(url + "/" + trackingId, string.Empty);

        var success = Convert.ToBoolean(response?.success);
        var message = Convert.ToString(response?.message);
      
        return new(success, message);
    }

    public AddressInfo ExtractAddressParts(string address)
    {
        // Split the address by commas
        var parts = address.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        // Create a new GhtkInfo object and map the parts
        var info = new AddressInfo
        {
            Address = parts.Length > 0 ? parts[0] : string.Empty,
            Ward = parts.Length > 1 ? parts[1] : string.Empty,
            District = parts.Length > 2 ? parts[2] : string.Empty,
            Province = parts.Length > 3 ? parts[3] : string.Empty,
        };

        return info;
    }

    #region private
    private async Task<dynamic?> PostAsync(string? url, string json)
    {
        try
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

            return JsonConvert.DeserializeObject<dynamic>(
                await response.Content.ReadAsStringAsync());
        }
        catch (Exception)
        {
            return null;
        }
    }

    private async Task<dynamic?> GetAsync(string? url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            return JsonConvert.DeserializeObject<dynamic>(
                await response.Content.ReadAsStringAsync());
        }
        catch (Exception)
        {
            return null;
        }
    }



    #endregion
}
