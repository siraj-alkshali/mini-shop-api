using MiniShop.Domain.Enums;

namespace MiniShop.Application.DTOs;

public record OrderDetails(
    int Id,
    string CustomerName,
    string CustomerEmail,
    OrderStatus Status,
    DiscountType DiscountType,
    decimal Subtotal,
    decimal ShippingCost,
    decimal Discount,
    decimal FinalAmount,
    string? CouponCode,
    IReadOnlyList<OrderItemDto> Items
);