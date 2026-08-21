using MiniShop.Application.DTOs.Products;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task<bool> DeleteAsync(int productId);
    Task<List<Product>> GetByIdsAsync(IEnumerable<int> productsIds);
    Task<(List<Product> Items, int TotalItems)> GetProductsAsync(ProductQueryParameters parameters);
}