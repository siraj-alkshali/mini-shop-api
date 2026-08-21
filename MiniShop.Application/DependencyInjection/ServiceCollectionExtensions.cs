using Microsoft.Extensions.DependencyInjection;
using MiniShop.Application.Discounts;
using MiniShop.Application.Discounts.Strategies;
using MiniShop.Application.Mapping;
using MiniShop.Application.Payments;
using MiniShop.Application.Services;
using MiniShop.Application.Shipping;
using MiniShop.Application.Shipping.Strategies;

namespace MiniShop.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(ProductProfile).Assembly);

        services.AddScoped<ProductService>();

        services.AddScoped<OrderService>();

        services.AddScoped<CustomerService>();

        services.AddScoped<DiscountResolver>();

        services.AddScoped<IDiscountStrategy, FirstOrderDiscountStrategy>();

        services.AddScoped<IDiscountStrategy, CouponDiscountStrategy>();

        services.AddScoped<ShippingResolver>();

        services.AddScoped<IShippingStrategy, StandardShippingStrategy>();

        services.AddScoped<IShippingStrategy, ExpressShippingStrategy>();

        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}