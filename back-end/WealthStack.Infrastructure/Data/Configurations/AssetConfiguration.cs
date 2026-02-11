using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WealthStack.Domain.Entities;

namespace WealthStack.Infrastructure.Data.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Ticker)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(a => a.Ticker).IsUnique();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Type)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.HasMany(a => a.History)
               .WithOne(h => h.Asset)
               .HasForeignKey(h => h.AssetId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.Navigation(a => a.History)
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .Metadata.SetField("_history");

        builder.HasMany(a => a.Transactions)
               .WithOne(t => t.Asset)
               .HasForeignKey(t => t.AssetId)
               .OnDelete(DeleteBehavior.Restrict);
        
        builder.Navigation(a => a.Transactions)
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .Metadata.SetField("_transactions");
    }
}