namespace MiniShop.Application.DTOs.Customers;

public record CustomerDto(
    int Id,
    int UserId,
    string Username,
    string Name,
    string Email
);