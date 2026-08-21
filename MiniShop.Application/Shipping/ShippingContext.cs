using MiniShop.Domain.Enums;

namespace MiniShop.Application.Shipping;

public record ShippingContext(
    ShippingMethod ShippingMethod,
    decimal OrderAmount
);