namespace MiniShop.Application.DTOs.Auth;

public record RegisterUserRequest(
    string Username,
    string Password,
    string Name,
    string Email);