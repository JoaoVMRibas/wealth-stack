using WealthStack.Domain.Entities;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    public Task AddAsync(Asset asset)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Asset>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Asset?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Asset?> GetByTickerAsync(string ticker)
    {
        throw new NotImplementedException();
    }

    public void Remove(Asset asset)
    {
        throw new NotImplementedException();
    }

    public void Update(Asset asset)
    {
        throw new NotImplementedException();
    }
}