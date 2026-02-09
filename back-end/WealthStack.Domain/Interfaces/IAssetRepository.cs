using WealthStack.Domain.Entities;

namespace WealthStack.Domain.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(int id);
    Task<Asset?> GetByTickerAsync(string ticker);
    Task<IEnumerable<Asset>> GetAllAsync();
    Task AddAsync(Asset asset);
    void Update(Asset asset);
    void Remove(Asset asset);
}