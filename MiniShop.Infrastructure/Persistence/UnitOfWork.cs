using MiniShop.Application.Interfaces;

namespace MiniShop.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly MiniShopDbContext _context;

    public UnitOfWork(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}