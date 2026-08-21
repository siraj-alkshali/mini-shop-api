using MiniShop.Domain.Enums;

namespace MiniShop.Application.Discounts;

public record DiscountResult(
    decimal Amount,
    DiscountType Type,
    int? CouponId
);