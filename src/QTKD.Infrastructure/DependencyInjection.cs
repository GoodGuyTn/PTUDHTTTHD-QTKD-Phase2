using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QTKD.Application.Abstractions;
using QTKD.Infrastructure.Persistence;
using QTKD.Infrastructure.Repositories;

namespace QTKD.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Đăng ký DbContext (PostgreSQL) và các repository.
    /// Chuỗi kết nối "ConnectionStrings:QtkdDb" KHÔNG nằm trong appsettings.json;
    /// lấy từ user-secrets hoặc biến môi trường ConnectionStrings__QtkdDb (xem README).
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("QtkdDb")
            ?? throw new InvalidOperationException(
                "Thiếu chuỗi kết nối 'ConnectionStrings:QtkdDb'. Cấu hình bằng dotnet user-secrets hoặc biến môi trường (xem README).");

        services.AddDbContext<QtkdDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        return services;
    }
}
