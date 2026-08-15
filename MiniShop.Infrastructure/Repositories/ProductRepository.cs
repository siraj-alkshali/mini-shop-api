using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Infrastructure.Persistence;

namespace MiniShop.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MiniShopDbContext _context;

    public ProductRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteAsync(int productId)
    {
        Product? product = await GetByIdAsync(productId);

        if (product is null)
            return false;

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return true;
    }
}