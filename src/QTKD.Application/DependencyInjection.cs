using Microsoft.Extensions.DependencyInjection;
using QTKD.Application.Services;

namespace QTKD.Application;

public static class DependencyInjection
{
    /// <summary>Đăng ký các service nghiệp vụ (Scoped: cùng vòng đời với DbContext).</summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IContractService, ContractService>();
        return services;
    }
}
