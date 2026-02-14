using Microsoft.AspNetCore.Mvc;
using WealthStack.Application.DTOs.Portfolios.Requests;
using WealthStack.Application.DTOs.Portfolios.Responses;
using WealthStack.Application.Interfaces;

namespace WealthStack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfoliosController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public PortfoliosController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpPost]
    public async Task<ActionResult<PortfolioResponse>> Create([FromBody]PortfolioRequest request)
    {
        var response = await _portfolioService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PortfolioResponse>> GetById(int id)
    {
        var response = await _portfolioService.GetByIdAsync(id);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PortfolioResponse>>> GetAll()
    {
        var response = await _portfolioService.GetAllAsync();
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PortfolioRequest request)
    {
        await _portfolioService.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _portfolioService.DeleteAsync(id);
        return NoContent();
    }
}