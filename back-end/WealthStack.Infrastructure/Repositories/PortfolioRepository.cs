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
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Portfolio>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Portfolio?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(Portfolio portfolio)
    {
        throw new NotImplementedException();
    }

    public void Update(Portfolio portfolio)
    {
        throw new NotImplementedException();
    }
}