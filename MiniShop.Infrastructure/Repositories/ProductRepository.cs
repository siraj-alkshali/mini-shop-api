using Microsoft.EntityFrameworkCore;
using MiniShop.Application.DTOs.Products;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Infrastructure.Persistence;
using MiniShop.Infrastructure.Persistence.Extensions;

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

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await _context.SaveChangesAsync();
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

    public async Task<List<Product>> GetByIdsAsync(IEnumerable<int> productsIds)
    {
        return await _context.Products
        .Where(product => productsIds.Contains(product.Id))
        .ToListAsync();
    }

    public async Task<(List<Product> Items, int TotalItems)> GetProductsAsync(ProductQueryParameters parameters)
    {
        IQueryable<Product> query = _context.Products
        .AsNoTracking()
        .ApplySearch(parameters.SearchTerm)
        .ApplyFilter(parameters)
        .ApplySort(parameters);

        int totalItems = await query.CountAsync();

        List<Product> items = await query
        .ApplyPagination(parameters)
        .ToListAsync();

        return (items, totalItems);
    }
}