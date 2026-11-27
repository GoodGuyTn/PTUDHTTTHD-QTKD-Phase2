using QTKD.Application.Dtos;

namespace QTKD.Application.Services;

public interface IContractService
{
    Task<ContractDto> GetByIdAsync(int id, CancellationToken ct = default);
}
