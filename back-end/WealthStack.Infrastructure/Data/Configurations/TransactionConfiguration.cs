using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WealthStack.Domain.Entities;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);

        builder.Ignore(t => t.TotalValue);

        builder.Property(t => t.Quantity)
               .HasPrecision(18, 8)
               .IsRequired();

        builder.Property(t => t.UnitPrice)
               .HasPrecision(18, 4)
               .IsRequired();
        
        builder.Property(t => t.Fees)
               .HasPrecision(18, 4)
               .IsRequired();
        
        builder.Property(t => t.TransactionDate)
               .IsRequired();
        
        builder.Property(t => t.Type)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();
        
        builder.Property(t => t.AssetId)
               .IsRequired();

        builder.Property(t => t.PortfolioId)
               .IsRequired();
       
       builder.HasOne(t => t.Asset)
               .WithMany(a => a.Transactions)
               .HasForeignKey(t => t.AssetId);
       
       builder.HasOne(t => t.Portfolio)
               .WithMany(p => p.Transactions)
               .HasForeignKey(t => t.PortfolioId);
    }
}