using WealthStack.Domain.Entities;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Infrastructure.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
    public Task AddAsync(Portfolio portfolio)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Portfolio>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Portfolio?> GetByIdAsync(int id)
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