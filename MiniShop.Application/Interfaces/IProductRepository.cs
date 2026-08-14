using MiniShop.Domain.Entities;

namespace MiniShop.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId);
}