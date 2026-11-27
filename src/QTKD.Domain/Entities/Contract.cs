using QTKD.Domain.Enums;

namespace QTKD.Domain.Entities;

/// <summary>Hợp đồng dịch vụ (ví dụ: HD-2026-018).</summary>
public class Contract
{
    public int Id { get; set; }

    /// <summary>Số hợp đồng, duy nhất trong hệ thống.</summary>
    public string ContractNo { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    /// <summary>Ngày bắt đầu hiệu lực (chỉ lưu ngày, không lưu giờ).</summary>
    public DateOnly StartDate { get; set; }

    /// <summary>Ngày kết thúc hiệu lực.</summary>
    public DateOnly EndDate { get; set; }

    /// <summary>Hợp đồng mới tạo luôn ở trạng thái Draft.</summary>
    public ContractStatus Status { get; set; } = ContractStatus.Draft;
}
