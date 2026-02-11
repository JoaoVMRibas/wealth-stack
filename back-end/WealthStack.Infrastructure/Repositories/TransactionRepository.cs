using WealthStack.Domain.Entities;
using WealthStack.Domain.Interfaces;
using WealthStack.Infrastructure.Data;

namespace WealthStack.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly WealthStackDbContext _context;

    public TransactionRepository(WealthStackDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Transaction>> GetByAssetIdAsync(int assetId)
    {
        throw new NotImplementedException();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Transaction>> GetByPortfolioIdAsync(int portfolioId)
    {
        throw new NotImplementedException();
    }

    public void Remove(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}