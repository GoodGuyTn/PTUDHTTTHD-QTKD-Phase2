using Microsoft.EntityFrameworkCore;
using QTKD.Domain.Entities;

namespace QTKD.Infrastructure.Persistence;

/// <summary>DbContext duy nhất của hệ thống QTKD, ánh xạ các entity sang bảng PostgreSQL.</summary>
public class QtkdDbContext(DbContextOptions<QtkdDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<PriceList> PriceLists => Set<PriceList>();
    public DbSet<PriceItem> PriceItems => Set<PriceItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Nạp toàn bộ lớp IEntityTypeConfiguration<T> trong thư mục Configurations
        // (mỗi entity một file: khoá, ràng buộc, độ dài cột, dữ liệu seed).
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QtkdDbContext).Assembly);
    }
}
