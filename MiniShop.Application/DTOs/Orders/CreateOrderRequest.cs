using MiniShop.Domain.Enums;

namespace MiniShop.Application.DTOs.Orders;

public record CreateOrderRequest(
    List<CreateOrderItemRequest> Items,
    int CustomerId,
    string? CouponCode,
    ShippingMethod ShippingMethod
    );