namespace MiniShop.Application.DTOs.Products;

public class CreateProductRequestDto
{
    public string Name { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }

    public CreateProductRequestDto(string name, int stockQuantity, decimal price)
    {
        Name = name;
        StockQuantity = stockQuantity;
        Price = price;
    }
}