using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;

namespace MiniShop.Infrastructure.Persistence.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(coupon => coupon.Id);

        builder.Property(coupon => coupon.Code).HasMaxLength(50).IsRequired();

        builder.Property(coupon => coupon.DiscountType).HasConversion<string>();

        builder.Property(coupon => coupon.DiscountValue).HasPrecision(10, 2);

        builder.Property(coupon => coupon.MinimumOrderAmount).HasPrecision(10, 2);

        builder.HasIndex(coupon => coupon.Code).IsUnique();

        builder.HasData(new Coupon
        {
            Id = 1,
            Code = "SUMMER10",
            DiscountType = DiscountCalculationType.Percentage,
            DiscountValue = 0.2m,
            MinimumOrderAmount = 20,
            ExpiresAt = new DateTime(2026, 9, 15, 0, 0, 0),
            UsageLimit = 200,
            IsActive = true
        });

        builder.HasData(new Coupon
        {
            Id = 2,
            Code = "SCHOOL5",
            DiscountType = DiscountCalculationType.FixedAmount,
            DiscountValue = 5,
            MinimumOrderAmount = 20,
            ExpiresAt = new DateTime(2026, 9, 1, 0, 0, 0),
            UsageLimit = 1000,
            IsActive = true
        });
    }
}