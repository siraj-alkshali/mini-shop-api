using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Interfaces;
using MiniShop.Infrastructure.Persistence;

namespace MiniShop.Infrastructure.Repositories;

public class CouponUsageRepository : ICouponUsageRepository
{
    private readonly MiniShopDbContext _context;

    public CouponUsageRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetCouponUsageCountAsync(int couponId)
    {
        return await _context.CouponUsages.CountAsync(couponUsage => couponUsage.CouponId == couponId);
    }

    public async Task<bool> HasCustomerUsedCouponAsync(int customerId, int couponId)
    {
        return await _context.CouponUsages.AnyAsync(couponUsage => couponUsage.CustomerId == customerId
        && couponUsage.CouponId == couponId);
    }
}