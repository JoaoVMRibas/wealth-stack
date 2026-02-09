using WealthStack.Domain.Entities;

namespace WealthStack.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(int id);
    Task<IEnumerable<Transaction>> GetByPortfolioIdAsync(int portfolioId);
    Task<IEnumerable<Transaction>> GetByAssetIdAsync(int assetId);
    Task AddAsync(Transaction transaction);
    void Remove(Transaction transaction);
}