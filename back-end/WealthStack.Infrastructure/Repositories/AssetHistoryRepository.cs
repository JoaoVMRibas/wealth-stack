using Microsoft.EntityFrameworkCore;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Interfaces;
using WealthStack.Infrastructure.Data;

namespace WealthStack.Infrastructure.Repositories;

public class AssetHistoryRepository : IAssetHistoryRepository
{
    private readonly WealthStackDbContext _context;

    public AssetHistoryRepository(WealthStackDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(AssetHistory assetHistory)
    {
        await _context.AssetHistories.AddAsync(assetHistory);
    }

    public async Task AddRangeAsync(IEnumerable<AssetHistory> historyRecords)
    {
        await _context.AssetHistories.AddRangeAsync(historyRecords);
    }

    public async Task<IEnumerable<AssetHistory>> GetHistoryByAssetIdAsync(int assetId)
    {
        return await _context.AssetHistories
                             .Where(h => h.AssetId == assetId)
                             .OrderByDescending(h => h.ReferenceDate)
                             .ToListAsync();
    }
}