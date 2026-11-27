namespace QTKD.Domain.Entities;

/// <summary>Bảng giá dịch vụ, có ngày bắt đầu áp dụng.</summary>
public class PriceList
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly EffectiveDate { get; set; }

    public ICollection<PriceItem> Items { get; set; } = new List<PriceItem>();
}
