namespace MiniShop.Application.Interfaces;

public interface ICouponUsageRepository
{
    Task<int> GetCouponUsageCountAsync(int couponId);
    Task<bool> HasCustomerUsedCouponAsync(int customerId, int couponId);
}