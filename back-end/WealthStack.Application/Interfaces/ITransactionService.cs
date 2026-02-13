using WealthStack.Application.DTOs.Transactions.Requests;
using WealthStack.Application.DTOs.Transactions.Responses;

namespace WealthStack.Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionResponse> CreateAsync(CreateTransactionRequest request);
    Task<TransactionResponse> GetByIdAsync(int id);
    Task<IEnumerable<TransactionResponse>> GetByPortfolioAsync(int portfolioId);
    Task<IEnumerable<TransactionResponse>> GetByAssetAsync(int assetId);
    Task DeleteAsync(int id);
}