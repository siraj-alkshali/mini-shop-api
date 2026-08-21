using MiniShop.Domain.Enums;
using MiniShop.Application.Common;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Discounts.Strategies;

public class CouponDiscountStrategy : IDiscountStrategy
{
    private readonly ICouponRepository _couponRepository;
    private readonly ICouponUsageRepository _couponUsageRepository;

    public CouponDiscountStrategy(ICouponRepository couponRepository, ICouponUsageRepository couponUsageRepository)
    {
        _couponRepository = couponRepository;
        _couponUsageRepository = couponUsageRepository;
    }

    private static bool IsCouponActive(Coupon coupon)
    {
        return coupon.IsActive;
    }

    private static bool IsCouponExpired(Coupon coupon)
    {
        if (coupon.ExpiresAt == null)
            return false;

        return coupon.ExpiresAt <= DateTime.Now;
    }

    private static bool OrderMeetsMinimumAmount(decimal amount, decimal? minimumOrderAmount)
    {
        if (minimumOrderAmount == null)
            return true;

        return amount >= minimumOrderAmount;
    }

    private async Task<bool> HasExceededUsageLimit(Coupon coupon)
    {
        if (coupon.UsageLimit == null)
            return false;

        int usageCount = await _couponUsageRepository.GetCouponUsageCountAsync(coupon.Id);

        return usageCount >= coupon.UsageLimit;
    }

    public async Task<ServiceResult<DiscountResult?>> TryApplyAsync(DiscountContext context)
    {
        if (context.CouponCode == null)
            return ServiceResult<DiscountResult?>.Success(null);

        Coupon? coupon = await _couponRepository.GetByCodeAsync(context.CouponCode);

        if (coupon == null)
            return ServiceResult<DiscountResult?>.Failure(["This coupon does not exist"], FailureType.NotFound);

        if (!IsCouponActive(coupon))
            return ServiceResult<DiscountResult?>.Failure(["This coupon is inactive"], FailureType.Conflict);

        if (IsCouponExpired(coupon))
            return ServiceResult<DiscountResult?>.Failure(["This coupon has expired"], FailureType.Conflict);

        if (!OrderMeetsMinimumAmount(context.OrderAmount, coupon.MinimumOrderAmount))
            return ServiceResult<DiscountResult?>.Failure(["This order doesn't meet the minimum order amount for the coupon applied"], FailureType.Conflict);

        if (await HasExceededUsageLimit(coupon))
            return ServiceResult<DiscountResult?>.Failure(["This coupon has exceeded its usage limit"], FailureType.Conflict);

        if (await _couponUsageRepository.HasCustomerUsedCouponAsync(context.CustomerId, coupon.Id))
            return ServiceResult<DiscountResult?>.Failure(["This coupon has already been used by this customer"], FailureType.Conflict);

        decimal discountAmount = coupon.DiscountType == DiscountCalculationType.Percentage
        ? context.OrderAmount * coupon.DiscountValue
        : Math.Min(context.OrderAmount, coupon.DiscountValue);

        return ServiceResult<DiscountResult?>.Success(new DiscountResult(discountAmount, DiscountType.Coupon, coupon.Id));
    }
}