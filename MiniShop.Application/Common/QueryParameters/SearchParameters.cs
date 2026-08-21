using MiniShop.Domain.Enums;

namespace MiniShop.Application.Common.QueryParameters;

public class SearchParameters : PaginationParameters
{
    public string? SearchTerm { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    public string? SortBy { get; set; }
}