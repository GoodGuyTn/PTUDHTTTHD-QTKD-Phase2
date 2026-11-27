using QTKD.Application.Abstractions;
using QTKD.Application.Dtos;
using QTKD.Application.Exceptions;

namespace QTKD.Application.Services;

/// <summary>Nghiệp vụ Contract. Tuần 06 sẽ bổ sung tạo/sửa/submit (US03, CTR-01..03).</summary>
public class ContractService(IContractRepository contracts) : IContractService
{
    public async Task<ContractDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var contract = await contracts.GetByIdAsync(id, ct)
            ?? throw new NotFoundException($"Không tìm thấy hợp đồng có Id = {id}.");

        return new ContractDto(
            contract.Id,
            contract.ContractNo,
            contract.CustomerId,
            contract.Customer?.Name ?? string.Empty,
            contract.StartDate,
            contract.EndDate,
            contract.Status.ToString());
    }
}
