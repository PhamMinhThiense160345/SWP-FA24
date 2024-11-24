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

 
}


