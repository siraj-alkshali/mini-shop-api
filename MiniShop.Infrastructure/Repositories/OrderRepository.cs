using Microsoft.EntityFrameworkCore;
using MiniShop.Application.DTOs;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using MiniShop.Infrastructure.Persistence;

namespace MiniShop.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly MiniShopDbContext _context;

    public OrderRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.AddAsync(order);
    }

    public async Task<bool> HasSuccessfulOrderAsync(int customerId)
    {
        return await _context.Orders.AnyAsync(order => order.CustomerId == customerId
        && order.Status == OrderStatus.Confirmed
        || order.Status == OrderStatus.Shipped
        || order.Status == OrderStatus.Delivered);
    }

    public async Task<Order?> GetByIdAsync(int orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }

    public async Task<OrderDetails?> GetOrderDetailsAsync(int orderId)
    {
        return await _context.Orders
        .AsNoTracking()
        .Where(order => order.Id == orderId)
        .Select(order => new OrderDetails(
            order.Id,
            order.Customer.Name,
            order.Customer.Email,
            order.Status,
            order.DiscountType,
            order.Items.Sum(item => item.Quantity * item.UnitPrice),
            order.ShippingCost,
            order.Discount,
            order.Items.Sum(item => item.Quantity * item.UnitPrice)
            + order.ShippingCost
            - order.Discount,
            order.CouponUsage != null ? order.CouponUsage.Coupon.Code : null,
            order.Items.Select(item => new OrderItemDto(
                item.ProductId,
                item.Product.Name,
                item.Quantity,
                item.UnitPrice,
                item.UnitPrice * item.Quantity
            )).ToList()
        )).SingleOrDefaultAsync();
    }
}