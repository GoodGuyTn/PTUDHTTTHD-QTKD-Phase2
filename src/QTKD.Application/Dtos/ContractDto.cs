namespace QTKD.Application.Dtos;

/// <summary>Chi tiết hợp đồng, kèm tên khách hàng để client không phải gọi thêm API.</summary>
public record ContractDto(
    int Id,
    string ContractNo,
    int CustomerId,
    string CustomerName,
    DateOnly StartDate,
    DateOnly EndDate,
    string Status);
