using QTKD.Application.Dtos;

namespace QTKD.Application.Services;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerDto>> GetAllAsync(CancellationToken ct = default);
}
