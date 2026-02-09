using WealthStack.Domain.Entities;

namespace WealthStack.Domain.Interfaces;

public interface IAssetHistoryRepository
{
    Task<IEnumerable<AssetHistory>> GetHistoryByAssetIdAsync(int assetId);
    Task AddAsync(AssetHistory assetHistory);
    Task AddRangeAsync(IEnumerable<AssetHistory> historyRecords);
}