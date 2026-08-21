using MiniShop.Application.Common;

namespace MiniShop.Application.Discounts;

public interface IDiscountStrategy
{
    Task<ServiceResult<DiscountResult?>> TryApplyAsync(DiscountContext context);
}