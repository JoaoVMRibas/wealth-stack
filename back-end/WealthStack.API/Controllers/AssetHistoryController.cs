using Microsoft.AspNetCore.Mvc;
using WealthStack.Application.DTOs.AssetHistory.Requests;
using WealthStack.Application.DTOs.AssetHistory.Responses;
using WealthStack.Application.Interfaces;

namespace WealthStack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetHistoryController : ControllerBase
{
    private readonly IAssetHistoryService _assetHistoryService;

    public AssetHistoryController(IAssetHistoryService assetHistoryService)
    {
        _assetHistoryService = assetHistoryService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateAssetHistoryRequest request)
    {
        await _assetHistoryService.CreateAsync(request);
        return Ok(new { message = "Asset history recorded successfully." });
    }

    [HttpGet("asset/{assetId}")]
    public async Task<ActionResult<IEnumerable<AssetHistoryResponse>>> GetByAsset(int assetId)
    {
        var response = await _assetHistoryService.GetByAssetAsync(assetId);
        return Ok(response);
    }
}