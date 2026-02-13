using Microsoft.EntityFrameworkCore;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Interfaces;
using WealthStack.Infrastructure.Data;

namespace WealthStack.Infrastructure.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly WealthStackDbContext _context;

    public PortfolioRepository(WealthStackDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
    }

    public async Task<IEnumerable<Portfolio>> GetAllAsync()
    {
        return await _context.Portfolios.ToListAsync();
    }

    public async Task<Portfolio?> GetByIdAsync(int id)
    {
        return await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Update(Portfolio portfolio)
    {
        _context.Update(portfolio);
    }

    public void Remove(Portfolio portfolio)
    {
        _context.Remove(portfolio);
    }
}