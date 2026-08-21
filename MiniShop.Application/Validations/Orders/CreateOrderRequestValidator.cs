using FluentValidation;
using MiniShop.Application.DTOs.Orders;

namespace MiniShop.Application.Validations.Orders;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Items).NotEmpty();

        RuleFor(x => x.ShippingMethod).NotEmpty();

        RuleForEach(x => x.Items).SetValidator(new CreateOrderItemRequestValidator());

        RuleFor(x => x.Items)
        .Must(items => items
        .Select(i => i.ProductId)
        .Distinct()
        .Count() == items.Count)
        .WithMessage("An order cannot contain the same product more than once");
    }
}