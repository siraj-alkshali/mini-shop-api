namespace MiniShop.Application.DTOs.Products;

public record CreateProductRequest(
    string Name,
    int StockQuantity,
    decimal Price
);
