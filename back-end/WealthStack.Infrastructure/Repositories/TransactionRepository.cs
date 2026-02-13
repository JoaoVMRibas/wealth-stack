using Microsoft.EntityFrameworkCore;
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
        await _context.Transactions.AddAsync(transaction);
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _context.Transactions
                             .Include(t => t.Asset)
                             .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transaction>> GetByAssetIdAsync(int assetId)
    {
        
        return await _context.Transactions
                             .Include(t => t.Asset)
                             .Where(t => t.AssetId == assetId)
                             .OrderByDescending(t => t.TransactionDate)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByPortfolioIdAsync(int portfolioId)
    {
        return await _context.Transactions
                             .Include(t => t.Asset)
                             .Where(t => t.PortfolioId == portfolioId)
                             .OrderByDescending(t => t.TransactionDate)
                             .ToListAsync();
    }

    public void Remove(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
    }
}