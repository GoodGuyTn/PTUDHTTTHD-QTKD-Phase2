using QTKD.Domain.Entities;
using QTKD.Domain.Enums;

namespace QTKD.Infrastructure.Persistence.Seed;

/// <summary>
/// Dữ liệu mẫu theo yêu cầu tuần 05: ≥2 Customer, 1 Contract Draft, 1 PriceList + 2 PriceItem.
/// Dùng Id cố định để EF Core đưa vào migration (HasData) — chạy "database update" là có dữ liệu.
/// Ví dụ bám theo tài liệu Week 02: Công ty Minh Phát, hợp đồng HD-2026-018, Port Services.
/// </summary>
internal static class SeedData
{
    public static readonly Customer[] Customers =
    [
        new() { Id = 1, Name = "Công ty Minh Phát", Status = CustomerStatus.Active },
        new() { Id = 2, Name = "Công ty TNHH Hải Long", Status = CustomerStatus.Active },
        new() { Id = 3, Name = "Công ty CP Vận tải Sao Mai", Status = CustomerStatus.Inactive }
    ];

    public static readonly Contract[] Contracts =
    [
        new()
        {
            Id = 1,
            ContractNo = "HD-2026-018",
            CustomerId = 1,
            StartDate = new DateOnly(2026, 10, 1),
            EndDate = new DateOnly(2027, 9, 30),
            Status = ContractStatus.Draft
        }
    ];

    public static readonly PriceList[] PriceLists =
    [
        new() { Id = 1, Name = "Bảng giá Port Services 2026", EffectiveDate = new DateOnly(2026, 10, 1) }
    ];

    public static readonly PriceItem[] PriceItems =
    [
        new() { PriceListId = 1, ServiceCode = "PS-LOAD", UnitPrice = 1_250_000m },   // Xếp dỡ container
        new() { PriceListId = 1, ServiceCode = "PS-STORAGE", UnitPrice = 85_000m }    // Lưu bãi / ngày
    ];
}
