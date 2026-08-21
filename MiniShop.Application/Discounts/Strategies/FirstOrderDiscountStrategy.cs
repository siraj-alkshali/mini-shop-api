using MiniShop.Application.Interfaces;
using Microsoft.Extensions.Options;
using MiniShop.Domain.Enums;
using MiniShop.Application.Common;

namespace MiniShop.Application.Discounts.Strategies;

public class FirstOrderDiscountStrategy : IDiscountStrategy
{
    private readonly IOrderRepository _orderRepository;
    private readonly DiscountOptions _options;

    public FirstOrderDiscountStrategy(IOrderRepository orderRepository, IOptions<DiscountOptions> options)
    {
        _orderRepository = orderRepository;
        _options = options.Value;
    }

    public async Task<ServiceResult<DiscountResult?>> TryApplyAsync(DiscountContext context)
    {
        if (await _orderRepository.HasSuccessfulOrderAsync(context.CustomerId))
            return ServiceResult<DiscountResult?>.Success(null);

        decimal discountAmount = context.OrderAmount * _options.FirstOrderPercentage;

        return ServiceResult<DiscountResult?>.Success(new DiscountResult(discountAmount, DiscountType.FirstOrder, null));
    }
}