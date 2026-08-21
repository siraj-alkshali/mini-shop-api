using MiniShop.Domain.Enums;

namespace MiniShop.Domain.Entities;

public class Coupon
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public DiscountCalculationType DiscountType { get; set; }

    public decimal DiscountValue { get; set; }

    public decimal? MinimumOrderAmount { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int? UsageLimit { get; set; }

    public bool IsActive { get; set; }

    // Navigation properties

    public ICollection<CouponUsage> CouponUsages { get; set; } = [];

}