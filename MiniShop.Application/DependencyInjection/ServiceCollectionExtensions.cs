using Microsoft.Extensions.DependencyInjection;
using MiniShop.Application.Services;

namespace MiniShop.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ProductService>();

        return services;
    }
}