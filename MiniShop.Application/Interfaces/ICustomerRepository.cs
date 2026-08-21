using MiniShop.Domain.Entities;

namespace MiniShop.Application.Interfaces;

public interface ICustomerRepository
{
    Task<bool> ExistsAsync(int customerId);
    Task<bool> EmailExistsAsync(string email);
}