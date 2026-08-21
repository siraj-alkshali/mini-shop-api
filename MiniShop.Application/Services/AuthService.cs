using AutoMapper;
using MiniShop.Application.Common;
using MiniShop.Application.DTOs.Auth;
using MiniShop.Application.DTOs.Customers;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Domain.enums;

namespace MiniShop.Application.Services;

public class AuthService : IAuthService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthService(ICustomerRepository customerRepository, IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    private async Task<ServiceResult> ValidateRequestInputAsync(RegisterUserRequest request)
    {
        if (await _customerRepository.EmailExistsAsync(request.Email))
            return ServiceResult.Failure(["This email already exists"], FailureType.Conflict);

        if (await _userRepository.UsernameExistsAsync(request.Username))
            return ServiceResult.Failure(["This username already exists"], FailureType.Conflict);

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<CustomerDto>> RegisterAsync(RegisterUserRequest request)
    {
        ServiceResult requestValidationResult = await ValidateRequestInputAsync(request);

        if (!requestValidationResult.IsSuccess)
            return ServiceResult<CustomerDto>.Failure(requestValidationResult.Errors, requestValidationResult.ResultType!.Value);

        User newUser = new User
        {
            Username = request.Username,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            IsActive = true,
            Role = Role.Customer,
            CreatedAt = DateTime.UtcNow
        };

        newUser.Customer = new Customer
        {
            User = newUser,
            Name = request.Name,
            Email = request.Email
        };

        await _userRepository.AddAsync(newUser);

        await _unitOfWork.SaveChangesAsync();

        CustomerDto customerDto = _mapper.Map<CustomerDto>(newUser.Customer);

        return ServiceResult<CustomerDto>.Success(customerDto);
    }
}