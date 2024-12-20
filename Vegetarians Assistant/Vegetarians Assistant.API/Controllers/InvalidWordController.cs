using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vegetarians_Assistant.API.Helpers;
using Vegetarians_Assistant.API.Requests;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.Admin;
using Vegetarians_Assistant.Services.Services.Interface.ArticleImp;
using Vegetarians_Assistant.Services.Services.Interface.IArticle;

namespace Vegetarians_Assistant.API.Controllers;

[Route("api/v1/invalid-word")]
[ApiController]
public class InvalidWordController(IInvalidWordService invalidWordService) : ControllerBase
{
    private readonly IInvalidWordService _invalidWordService = invalidWordService;

    [HttpGet("getall")] 
    public async Task<IActionResult> GetAllInvalidWord()
    {
        var list = await _invalidWordService.GetAllAsync();
        return Ok(list.OrderBy(x => x.Content));
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddInvalidWord([FromBody] InValidWordRequest request)
    {
        var result = await _invalidWordService.AddAsync(request.Content);
        return Ok(result);
    }

  
}


