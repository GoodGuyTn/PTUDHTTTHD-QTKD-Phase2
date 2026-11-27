using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTKD.Domain.Entities;
using QTKD.Infrastructure.Persistence.Seed;

namespace QTKD.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);
        // Seed dùng Id cố định 1..3 nên cho sequence bắt đầu từ 100,
        // tránh lỗi trùng khoá khi tạo khách hàng mới qua API.
        builder.Property(c => c.Id).HasIdentityOptions(startValue: 100);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);

        // Lưu enum dạng chuỗi ("Active") để khi xem DB dễ đọc hơn số 0/1.
        builder.Property(c => c.Status).HasConversion<string>().HasMaxLength(20);

        builder.HasData(SeedData.Customers);
    }
}
