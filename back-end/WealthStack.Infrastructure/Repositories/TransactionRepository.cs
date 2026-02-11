using WealthStack.Domain.Entities;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public Task AddAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetByAssetIdAsync(int assetId)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetByPortfolioIdAsync(int portfolioId)
    {
        throw new NotImplementedException();
    }

    public void Remove(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}