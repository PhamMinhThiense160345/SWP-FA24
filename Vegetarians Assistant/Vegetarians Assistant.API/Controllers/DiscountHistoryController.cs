using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.DiscountHistories;

namespace Vegetarians_Assistant.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountHistoryController(IDiscountHistoryService discountHistoryService) : ControllerBase
{
    private readonly IDiscountHistoryService _discountHistoryService = discountHistoryService;

    [HttpPost("/api/v1/discount-history")]
    public async Task<IActionResult> AddAsync([FromBody] DiscountHistoryView request)
    {
        var discount = request.MapToDiscountHistory();
        var result = await _discountHistoryService.AddAsync(discount);
        return Ok(new ResponseView(result.Item1, result.Item2));
    }
    [HttpGet("/api/v1/discount-history/inactive/{userId}/{tierId}")]
    public async Task<IActionResult> InactiveAsync([FromRoute] int userId, int tierId)
    {
        var result = await _discountHistoryService.UpdateStatusAsync(userId, tierId, "inactive");
        return Ok(new ResponseView(result.Item1, result.Item2));
    }

    [HttpGet("/api/v1/discount-history/deactive/{userId}/{tierId}")]
    public async Task<IActionResult> DeactiveAsync([FromRoute] int userId, int tierId)
    {
        var result = await _discountHistoryService.UpdateStatusAsync(userId, tierId, "deactive");
        return Ok(new ResponseView(result.Item1, result.Item2));
    }

    [HttpGet("/api/v1/discount-history/{userId}")]
    public async Task<IActionResult> GetByUserIdAsync([FromRoute] int userId)
    {
        var result = await _discountHistoryService.GetByUserIdAsync(userId);
        return Ok(result);
    }

}


