using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Infrastructure.Persistence;

namespace MiniShop.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly MiniShopDbContext _context;

    public CustomerRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(int customerId)
    {
        return await _context.Customers.AnyAsync(customer => customer.Id == customerId);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Customers.AnyAsync(customer => customer.Email == email);
    }
}