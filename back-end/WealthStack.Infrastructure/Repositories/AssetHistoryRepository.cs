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
        throw new NotImplementedException();
    }

    public async Task AddRangeAsync(IEnumerable<AssetHistory> historyRecords)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AssetHistory>> GetHistoryByAssetIdAsync(int assetId)
    {
        throw new NotImplementedException();
    }
}