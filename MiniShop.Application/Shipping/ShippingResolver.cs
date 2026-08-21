using MiniShop.Application.Common;
using MiniShop.Domain.Enums;

namespace MiniShop.Application.Shipping;

public class ShippingResolver
{
    private readonly IReadOnlyDictionary<ShippingMethod, IShippingStrategy> _strategiesDict;

    public ShippingResolver(IEnumerable<IShippingStrategy> strategies)
    {
        _strategiesDict = strategies.ToDictionary(strategy => strategy.ShippingMethod);
    }

    public ServiceResult<decimal> ResolveShippingCost(ShippingContext shippingContext)
    {
        if (!_strategiesDict.TryGetValue(shippingContext.ShippingMethod,
        out IShippingStrategy? shippingStrategy))
            return ServiceResult<decimal>.Failure(["This shipping method does not exist"], FailureType.BadRequest);

        decimal shippingCost = shippingStrategy.CalculateShippingCost(shippingContext);

        return ServiceResult<decimal>.Success(shippingCost);
    }
}