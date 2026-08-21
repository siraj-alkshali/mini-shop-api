namespace MiniShop.Application.DTOs.Products;

public interface IProductRequest
{
    string Name { get; }
    int StockQuantity { get; }
    decimal Price { get; }
}