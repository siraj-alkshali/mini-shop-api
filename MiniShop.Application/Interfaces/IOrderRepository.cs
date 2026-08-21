using MiniShop.Application.DTOs;
using MiniShop.Domain.Entities;

namespace MiniShop.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<bool> HasSuccessfulOrderAsync(int customerId);
    Task<OrderDetails?> GetOrderDetailsAsync(int orderId);
    Task<Order?> GetByIdAsync(int orderId);
}