using WealthStack.Domain.Entities;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Infrastructure.Repositories;

public class AssetHistoryRepository : IAssetHistoryRepository
{
    public Task AddAsync(AssetHistory assetHistory)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<AssetHistory> historyRecords)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AssetHistory>> GetHistoryByAssetIdAsync(int assetId)
    {
        throw new NotImplementedException();
    }
}