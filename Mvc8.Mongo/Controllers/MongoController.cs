using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Mvc8.Mongo.Services;

namespace Mvc8.Mongo.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Experimental("SKEXP0001")]
public class MongoController : ControllerBase
{
    private readonly SemanticKernelService _semanticKernelService;

    public MongoController(SemanticKernelService semanticKernelService)
    {
        _semanticKernelService = semanticKernelService;
    }

    public async Task<IActionResult> InitializeVectorDb()
    {
        await _semanticKernelService.FetchAndSaveProductDocumentsAsync(170);
        return Ok();
    }
    
    public async Task<IActionResult> GetRecommendationsAsync([FromQuery]string userInput)
    {
        var result = await _semanticKernelService.GetRecommendationsAsync(userInput);
        return Ok(result);
    }
}