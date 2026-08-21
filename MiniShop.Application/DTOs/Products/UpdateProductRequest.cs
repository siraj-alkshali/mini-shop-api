namespace MiniShop.Application.DTOs.Products;

public record UpdateProductRequest(
    string Name,
    int StockQuantity,
    decimal Price
) : IProductRequest;