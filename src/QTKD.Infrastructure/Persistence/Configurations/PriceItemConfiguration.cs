using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTKD.Domain.Entities;
using QTKD.Infrastructure.Persistence.Seed;

namespace QTKD.Infrastructure.Persistence.Configurations;

public class PriceItemConfiguration : IEntityTypeConfiguration<PriceItem>
{
    public void Configure(EntityTypeBuilder<PriceItem> builder)
    {
        builder.ToTable("PriceItems", t =>
            t.HasCheckConstraint("CK_PriceItems_UnitPrice_NonNegative", "\"UnitPrice\" >= 0"));

        // Khoá chính ghép theo đề: (PriceListId, ServiceCode).
        builder.HasKey(i => new { i.PriceListId, i.ServiceCode });
        builder.Property(i => i.ServiceCode).HasMaxLength(50);
        builder.Property(i => i.UnitPrice).HasPrecision(18, 2);

        builder.HasData(SeedData.PriceItems);
    }
}
