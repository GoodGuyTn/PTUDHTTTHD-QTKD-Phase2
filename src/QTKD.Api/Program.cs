using QTKD.Api.Middleware;
using QTKD.Application;
using QTKD.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Controllers + Swagger (tài liệu API, thử endpoint ngay trên trình duyệt)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Đăng ký các tầng theo kiến trúc phân lớp:
//    Api (Controller) → Application (Service, interface Repository) → Infrastructure (EF Core, PostgreSQL)
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// 3. Middleware bắt lỗi toàn cục đặt đầu pipeline để bao toàn bộ request.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;   // Swagger UI mở tại http://localhost:5080/
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "QTKD API v1");
    });
}

app.MapControllers();

app.Run();

// Cho phép project integration test (tuần 07) dùng WebApplicationFactory<Program>.
public partial class Program;
