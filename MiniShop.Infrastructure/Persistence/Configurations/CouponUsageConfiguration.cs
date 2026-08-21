using MiniShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiniShop.Infrastructure.Persistence.Configurations;

public class CouponUsageConfiguration : IEntityTypeConfiguration<CouponUsage>
{
    public void Configure(EntityTypeBuilder<CouponUsage> builder)
    {
        builder.HasKey(couponUsage => couponUsage.Id);

        builder.HasOne(couponUsage => couponUsage.Customer)
        .WithMany(customer => customer.CouponUsages)
        .HasForeignKey(couponUsage => couponUsage.CustomerId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();

        builder.HasOne(couponUsage => couponUsage.Order)
        .WithOne(order => order.CouponUsage)
        .HasForeignKey<CouponUsage>(couponUsage => couponUsage.OrderId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();

        builder.HasOne(couponUsage => couponUsage.Coupon)
        .WithMany(coupon => coupon.CouponUsages)
        .HasForeignKey(couponUsage => couponUsage.CouponId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();

        builder.HasIndex(cu => cu.OrderId).IsUnique();
    }
}