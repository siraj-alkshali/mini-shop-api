using MiniShop.Domain.Entities;

namespace MiniShop.Application.Interfaces;

public interface IUserRepository
{
    Task<bool> UsernameExistsAsync(string username);
    Task AddAsync(User user);
}