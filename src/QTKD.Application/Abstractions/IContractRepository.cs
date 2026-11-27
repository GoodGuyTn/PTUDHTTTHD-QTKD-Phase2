using QTKD.Domain.Entities;

namespace QTKD.Application.Abstractions;

public interface IContractRepository
{
    /// <summary>Lấy hợp đồng theo Id, kèm Customer. Trả về null nếu không có.</summary>
    Task<Contract?> GetByIdAsync(int id, CancellationToken ct = default);
}
