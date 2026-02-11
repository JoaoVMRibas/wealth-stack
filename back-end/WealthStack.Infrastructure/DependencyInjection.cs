using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WealthStack.Domain.Interfaces;
using WealthStack.Infrastructure.Data;
using WealthStack.Infrastructure.Repositories;

namespace WealthStack.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<WealthStackDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddScoped<IAssetRepository, AssetRepository>();

        return services;
    }
}