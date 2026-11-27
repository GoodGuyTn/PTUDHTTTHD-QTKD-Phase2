using QTKD.Application.Abstractions;
using QTKD.Application.Dtos;

namespace QTKD.Application.Services;

/// <summary>Nghiệp vụ Customer. Tuần 05 mới có đọc danh sách; tạo/sửa bổ sung ở tuần 06.</summary>
public class CustomerService(ICustomerRepository customers) : ICustomerService
{
    public async Task<IReadOnlyList<CustomerDto>> GetAllAsync(CancellationToken ct = default)
    {
        var list = await customers.GetAllAsync(ct);
        return list.Select(c => new CustomerDto(c.Id, c.Name, c.Status.ToString())).ToList();
    }
}
