using Microsoft.AspNetCore.Mvc;
using WealthStack.Application.DTOs.Assets.Requests;
using WealthStack.Application.DTOs.Assets.Responses;
using WealthStack.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly IAssetService _assetService;

    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpPost]
    public async Task<ActionResult<AssetResponse>> Create([FromBody]CreateAssetRequest request)
    {
        var response = await _assetService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetResponse>> GetById(int id)
    {
        var response = await _assetService.GetByIdAsync(id);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetResponse>>> GetAll()
    {
        var assets = await _assetService.GetAllAsync();

        return Ok(assets);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateAssetRequest request)
    {
        await _assetService.UpdateAsync(id,request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _assetService.DeleteAsync(id);
        return NoContent();
    }
}