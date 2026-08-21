using MiniShop.Application.Common.QueryParameters;

namespace MiniShop.Application.DTOs.Products;

public class ProductQueryParameters : SearchParameters
{
    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }
}