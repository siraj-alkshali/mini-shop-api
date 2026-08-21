using MiniShop.Application.Common;
using MiniShop.Domain.Enums;

namespace MiniShop.Application.Discounts;

public class DiscountResolver
{
    private readonly IEnumerable<IDiscountStrategy> _strategies;

    public DiscountResolver(IEnumerable<IDiscountStrategy> strategies)
    {
        _strategies = strategies;
    }

    public async Task<ServiceResult<DiscountResult?>> ResolveAsync(DiscountContext context)
    {
        foreach (IDiscountStrategy strategy in _strategies)
        {
            ServiceResult<DiscountResult?> result = await strategy.TryApplyAsync(context);

            if (!result.IsSuccess)
                return result;

            if (result.Data is not null)
                return result;
        }

        return ServiceResult<DiscountResult?>.Success(null);
    }
}