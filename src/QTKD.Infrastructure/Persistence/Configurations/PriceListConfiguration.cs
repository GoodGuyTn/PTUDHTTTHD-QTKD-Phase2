using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTKD.Domain.Entities;
using QTKD.Infrastructure.Persistence.Seed;

namespace QTKD.Infrastructure.Persistence.Configurations;

public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
{
    public void Configure(EntityTypeBuilder<PriceList> builder)
    {
        builder.ToTable("PriceLists");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasIdentityOptions(startValue: 100);   // Seed dùng Id = 1
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

        builder.HasMany(p => p.Items)
               .WithOne(i => i.PriceList)
               .HasForeignKey(i => i.PriceListId)
               .OnDelete(DeleteBehavior.Cascade);   // Xoá bảng giá thì xoá luôn các dòng giá

        builder.HasData(SeedData.PriceLists);
    }
}
