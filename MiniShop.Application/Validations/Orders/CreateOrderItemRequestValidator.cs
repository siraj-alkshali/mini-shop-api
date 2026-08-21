using FluentValidation;
using MiniShop.Application.DTOs.Orders;

namespace MiniShop.Application.Validations.Orders;

public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemRequestValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);

        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}