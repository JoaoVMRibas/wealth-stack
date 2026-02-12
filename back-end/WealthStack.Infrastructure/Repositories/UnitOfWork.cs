using WealthStack.Domain.Interfaces;
using WealthStack.Infrastructure.Data;

namespace WealthStack.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly WealthStackDbContext _context;
    public UnitOfWork(WealthStackDbContext context) => _context = context;

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
}