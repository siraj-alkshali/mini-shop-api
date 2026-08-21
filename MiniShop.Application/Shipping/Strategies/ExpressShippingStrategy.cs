using MiniShop.Domain.Enums;
using Microsoft.Extensions.Options;

namespace MiniShop.Application.Shipping.Strategies;

public class ExpressShippingStrategy : IShippingStrategy
{
    private readonly ShippingOptions _options;

    public ExpressShippingStrategy(IOptions<ShippingOptions> options)
    {
        _options = options.Value;
    }

    public ShippingMethod ShippingMethod => ShippingMethod.Express;

    public decimal CalculateShippingCost(ShippingContext context)
    {
        return _options.ExpressShippingCost;
    }
}