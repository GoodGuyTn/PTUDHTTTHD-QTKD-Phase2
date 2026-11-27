namespace QTKD.Domain.Entities;

/// <summary>
/// Một dòng giá trong bảng giá. Khoá chính ghép (PriceListId, ServiceCode):
/// mỗi mã dịch vụ chỉ xuất hiện một lần trong một bảng giá.
/// </summary>
public class PriceItem
{
    public int PriceListId { get; set; }
    public PriceList? PriceList { get; set; }

    public string ServiceCode { get; set; } = string.Empty;

    /// <summary>Đơn giá (VND).</summary>
    public decimal UnitPrice { get; set; }
}
