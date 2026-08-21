using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<bool> ExistsAsync(int customerId)
    {
        return await _customerRepository.ExistsAsync(customerId);
    }
}