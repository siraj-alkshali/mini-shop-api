using MiniShop.Domain.Entities;

namespace MiniShop.Application.Interfaces;

public interface ICouponRepository
{
    Task<Coupon?> GetByCodeAsync(string couponCode);
}