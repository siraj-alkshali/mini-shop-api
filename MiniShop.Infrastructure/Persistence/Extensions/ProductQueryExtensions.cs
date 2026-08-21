using MiniShop.Application.DTOs.Products;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;

namespace MiniShop.Infrastructure.Persistence.Extensions;

public static class ProductQueryExtensions
{
    public static IQueryable<Product> ApplySearch(this IQueryable<Product> query, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return query;

        return query.Where(p => p.Name.Contains(searchTerm.Trim()));
    }

    public static IQueryable<Product> ApplyFilter(this IQueryable<Product> query, ProductQueryParameters parameters)
    {
        if (parameters == null)
            return query;

        if (parameters.MinPrice.HasValue)
            query = query.Where(p => p.Price >= parameters.MinPrice.Value);

        if (parameters.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= parameters.MaxPrice.Value);

        return query;
    }

    public static IQueryable<Product> ApplySort(this IQueryable<Product> query, ProductQueryParameters parameters)
    {
        query = parameters.SortBy?.ToLower() switch
        {
            "price" => parameters.SortDirection == SortDirection.Desc
                ? query.OrderByDescending(product => product.Price)
                : query.OrderBy(product => product.Price),

            "name" => parameters.SortDirection == SortDirection.Desc
                ? query.OrderByDescending(product => product.Name)
                : query.OrderBy(product => product.Name),

            _ => query.OrderBy(product => product.Id)
        };

        return query;
    }
}