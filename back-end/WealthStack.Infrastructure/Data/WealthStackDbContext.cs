using Microsoft.EntityFrameworkCore;
using WealthStack.Domain.Entities;

namespace WealthStack.Infrastructure.Data;

public class WealthStackDbContext : DbContext
{
    public WealthStackDbContext(DbContextOptions<WealthStackDbContext> options) 
        : base(options) {}

    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Portfolio> Portfolios => Set<Portfolio>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<AssetHistory> AssetHistories => Set<AssetHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WealthStackDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}