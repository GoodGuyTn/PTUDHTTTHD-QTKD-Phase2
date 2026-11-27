using QTKD.Domain.Enums;

namespace QTKD.Domain.Entities;

/// <summary>Khách hàng của công ty (ví dụ: Công ty Minh Phát).</summary>
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CustomerStatus Status { get; set; } = CustomerStatus.Active;

    // Navigation: một khách hàng có nhiều hợp đồng.
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
