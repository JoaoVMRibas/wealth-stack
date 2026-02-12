using Microsoft.AspNetCore.Mvc;
using WealthStack.Application.DTOs.Assets.Requests;
using WealthStack.Application.DTOs.Assets.Responses;
using WealthStack.Application.Interfaces;
using WealthStack.Domain.Exceptions;

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
        try
        {
            var response = await _assetService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);   
        }
        catch(ApplicationException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetResponse>> GetById(int id)
    {
        try
        {
            var response = await _assetService.GetByIdAsync(id);
            return Ok(response);   
        }
        catch(DomainException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetResponse>>> GetAll()
    {
        var assets = await _assetService.GetAllAsync();
        return Ok(assets);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAssetRequest request)
    {
        try
        {
            await _assetService.UpdateAsync(id, request);
            return NoContent();
        }
        catch (DomainException exception)
        {
            return NotFound(new { message = exception.Message });
        }
        catch (ApplicationException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _assetService.DeleteAsync(id);
            return NoContent();
        }
        catch (AssetNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}