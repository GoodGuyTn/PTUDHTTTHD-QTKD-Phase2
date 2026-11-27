namespace QTKD.Application.Dtos;

/// <summary>Dữ liệu khách hàng trả về cho client (không lộ entity ra ngoài API).</summary>
public record CustomerDto(int Id, string Name, string Status);
