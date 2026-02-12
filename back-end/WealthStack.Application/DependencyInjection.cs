using Microsoft.Extensions.DependencyInjection;
using WealthStack.Application.Interfaces;
using WealthStack.Application.Services;
using WealthStack.Domain.Services;

namespace WealthStack.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<TransactionDomainService>();
        services.AddScoped<IAssetService, AssetService>();
        return services;
    }
}