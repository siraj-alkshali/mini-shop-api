using MiniShop.Application.Common.QueryParameters;

namespace MiniShop.Infrastructure.Persistence.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationParameters paginationParameters)
    {
        return query.Skip(paginationParameters.Skip).Take(paginationParameters.PageSize);
    }
}