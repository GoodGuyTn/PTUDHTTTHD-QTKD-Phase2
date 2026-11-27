using Microsoft.AspNetCore.Mvc;
using QTKD.Application.Exceptions;

namespace QTKD.Api.Middleware;

/// <summary>
/// Bắt mọi exception chưa xử lý và trả về JSON theo chuẩn ProblemDetails (RFC 7807),
/// để client luôn nhận được lỗi rõ ràng thay vì trang lỗi HTML.
/// </summary>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // Nếu response đã bắt đầu gửi về client thì không thể đổi status code nữa.
            if (context.Response.HasStarted) throw;

            var (status, title) = ex switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            if (status == StatusCodes.Status500InternalServerError)
                logger.LogError(ex, "Lỗi không mong muốn khi xử lý {Path}", context.Request.Path);

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                // Không trả chi tiết lỗi hệ thống (500) ra ngoài để tránh lộ thông tin nội bộ.
                Detail = status == StatusCodes.Status500InternalServerError
                    ? "Đã xảy ra lỗi hệ thống."
                    : ex.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(problem, options: null, contentType: "application/problem+json");
        }
    }
}
