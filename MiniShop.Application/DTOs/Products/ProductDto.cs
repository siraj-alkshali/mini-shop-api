namespace MiniShop.Application.DTOs.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }

    public ProductDto(int id, string name, int stockQuantity, decimal price)
    {
        Id = id;
        Name = name;
        StockQuantity = stockQuantity;
        Price = price;
    }
}