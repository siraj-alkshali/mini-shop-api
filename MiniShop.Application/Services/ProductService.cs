using MiniShop.Application.DTOs.Products;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> GetByIdAsync(int productId)
    {
        Product? product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
            return null;

        return new ProductDto(
            product.Id,
            product.Name,
            product.StockQuantity,
            product.Price
        );
    }
}