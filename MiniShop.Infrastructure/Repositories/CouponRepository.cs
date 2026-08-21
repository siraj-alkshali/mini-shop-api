using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Infrastructure.Persistence;

namespace MiniShop.Infrastructure.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly MiniShopDbContext _context;

    public CouponRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task<Coupon?> GetByCodeAsync(string couponCode)
    {
        return await _context.Coupons.SingleOrDefaultAsync(coupon => coupon.Code == couponCode);
    }
}