using MiniShop.Domain.Enums;

namespace MiniShop.Application.Shipping;

public interface IShippingStrategy
{
    ShippingMethod ShippingMethod { get; }

    decimal CalculateShippingCost(ShippingContext context);
}