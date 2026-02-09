using WealthStack.Domain.Entities;

namespace WealthStack.Domain.Interfaces;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByIdAsync(int id);
    Task<IEnumerable<Portfolio>> GetAllAsync();
    Task AddAsync(Portfolio portfolio);
    void Update(Portfolio portfolio);
    void Remove(Portfolio portfolio);
}