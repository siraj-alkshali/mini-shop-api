using MiniShop.Application.Common;
using MiniShop.Application.DTOs.Auth;
using MiniShop.Application.DTOs.Customers;

namespace MiniShop.Application.Interfaces;

public interface IAuthService
{
    Task<ServiceResult<CustomerDto>> RegisterAsync(RegisterUserRequest request);
}