using MiniShop.Domain.Enums;
using Microsoft.Extensions.Options;

namespace MiniShop.Application.Shipping.Strategies;

public class StandardShippingStrategy : IShippingStrategy
{
    private readonly ShippingOptions _options;

    public StandardShippingStrategy(IOptions<ShippingOptions> options)
    {
        _options = options.Value;
    }

    public ShippingMethod ShippingMethod => ShippingMethod.Standard;

    public decimal CalculateShippingCost(ShippingContext context)
    {
        return context.OrderAmount >= _options.FreeShippingThreshold ? 0 : _options.StandardShippingCost;
    }
}