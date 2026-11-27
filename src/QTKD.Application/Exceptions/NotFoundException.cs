namespace QTKD.Application.Exceptions;

/// <summary>Ném ra khi không tìm thấy bản ghi; middleware chuyển thành HTTP 404.</summary>
public class NotFoundException(string message) : Exception(message);
