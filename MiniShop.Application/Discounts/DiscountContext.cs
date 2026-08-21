namespace MiniShop.Application.Discounts;

public record DiscountContext(
    int CustomerId,
    decimal OrderAmount,
    string? CouponCode
);