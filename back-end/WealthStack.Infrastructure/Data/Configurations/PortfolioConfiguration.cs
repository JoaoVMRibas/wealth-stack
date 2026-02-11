using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WealthStack.Domain.Entities;

namespace WealthStack.Infrastructure.Data.Configurations;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("Portfolios");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .HasMaxLength(100)
               .IsRequired();
        
        builder.Property(p => p.Description)
               .HasMaxLength(500);
        
        builder.HasMany(p => p.Transactions)
               .WithOne(t => t.Portfolio)
               .HasForeignKey(t => t.PortfolioId)
               .OnDelete(DeleteBehavior.Restrict);
        
        builder.Navigation(p => p.Transactions)
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .Metadata.SetField("_transactions");
    }
}