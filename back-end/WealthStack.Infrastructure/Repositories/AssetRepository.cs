using WealthStack.Domain.Entities;
using WealthStack.Domain.Interfaces;
using WealthStack.Infrastructure.Data;

namespace WealthStack.Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly WealthStackDbContext _context;

    public AssetRepository(WealthStackDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Asset asset)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Asset>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Asset?> GetByTickerAsync(string ticker)
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