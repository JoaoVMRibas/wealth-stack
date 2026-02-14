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
}