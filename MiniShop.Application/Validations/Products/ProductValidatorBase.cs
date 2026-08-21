using FluentValidation;
using MiniShop.Application.DTOs.Products;

namespace MiniShop.Application.Validations;

public abstract class ProductValidatorBase<T> : AbstractValidator<T> where T : IProductRequest
{
    protected ProductValidatorBase()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);

        RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price).GreaterThan(0);
    }
}