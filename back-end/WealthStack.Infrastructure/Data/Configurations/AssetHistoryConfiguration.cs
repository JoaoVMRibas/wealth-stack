using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WealthStack.Domain.Entities;

namespace WealthStack.Infrastructure.Data.Configurations;

public class AssetHistoryConfiguration : IEntityTypeConfiguration<AssetHistory>
{
    public void Configure(EntityTypeBuilder<AssetHistory> builder)
    {
        builder.ToTable("AssetHistories");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.ReferenceDate)
               .IsRequired();
        
        builder.Property(a => a.Price)
               .HasPrecision(18,4)
               .IsRequired();
        
        builder.HasOne(h => h.Asset)
               .WithMany(a => a.History)
               .HasForeignKey(h => h.AssetId);
    }
}