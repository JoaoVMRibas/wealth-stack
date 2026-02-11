using Microsoft.EntityFrameworkCore;
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
        await _context.Assets.AddAsync(asset);
    }

    public async Task<IEnumerable<Asset>> GetAllAsync()
    {
        return await _context.Assets.ToListAsync();
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        return await _context.Assets.FindAsync(id);
    }

    public async Task<Asset?> GetByTickerAsync(string ticker)
    {
        return await _context.Assets.FirstOrDefaultAsync(a => a.Ticker == ticker);
    }

    public void Update(Asset asset)
    {
        _context.Assets.Update(asset);
    }

    public void Remove(Asset asset)
    {
        _context.Assets.Remove(asset);
    }
}