using Microsoft.Extensions.DependencyInjection;
using MiniShop.Application.Mapping;
using MiniShop.Application.Services;

namespace MiniShop.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(ProductProfile).Assembly);

        services.AddScoped<ProductService>();

        return services;
    }
}