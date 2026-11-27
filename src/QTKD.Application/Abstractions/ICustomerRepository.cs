using QTKD.Domain.Entities;

namespace QTKD.Application.Abstractions;

/// <summary>
/// Hợp đồng truy cập dữ liệu Customer. Tầng Application chỉ biết interface này;
/// cài đặt bằng EF Core nằm ở QTKD.Infrastructure.
/// </summary>
public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken ct = default);
}
