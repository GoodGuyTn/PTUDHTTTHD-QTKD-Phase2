using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTKD.Domain.Entities;
using QTKD.Infrastructure.Persistence.Seed;

namespace QTKD.Infrastructure.Persistence.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contracts", t =>
            // Ràng buộc ở mức DB: ngày kết thúc không được trước ngày bắt đầu.
            t.HasCheckConstraint("CK_Contracts_EndDate_After_StartDate", "\"EndDate\" >= \"StartDate\""));

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasIdentityOptions(startValue: 100);   // Seed dùng Id = 1
        builder.Property(c => c.ContractNo).IsRequired().HasMaxLength(50);
        builder.HasIndex(c => c.ContractNo).IsUnique();   // Số hợp đồng không trùng
        builder.Property(c => c.Status).HasConversion<string>().HasMaxLength(30);

        // Mỗi hợp đồng thuộc một khách hàng; không cho xoá khách hàng đang có hợp đồng.
        builder.HasOne(c => c.Customer)
               .WithMany(cu => cu.Contracts)
               .HasForeignKey(c => c.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(SeedData.Contracts);
    }
}
