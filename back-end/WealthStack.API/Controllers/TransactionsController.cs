using Microsoft.AspNetCore.Mvc;
using WealthStack.Application.DTOs.Transactions.Requests;
using WealthStack.Application.DTOs.Transactions.Responses;
using WealthStack.Application.Interfaces;

namespace WealthStack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponse>> Create([FromBody] CreateTransactionRequest request)
    {
        var response = await _transactionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionResponse>> GetById(int id)
    {
        var response = await _transactionService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpGet("portfolio/{portfolioId}")]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetByPortfolio(int portfolioId)
    {
        var transactions = await _transactionService.GetByPortfolioAsync(portfolioId);
        return Ok(transactions);
    }

    [HttpGet("asset/{assetId}")]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetByAsset(int assetId)
    {
        var transactions = await _transactionService.GetByAssetAsync(assetId);
        return Ok(transactions);
    }
}