using MiniShop.Domain.Enums;

namespace MiniShop.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public OrderStatus Status { get; set; }

    public DiscountType DiscountType { get; set; }

    public decimal ShippingCost { get; set; }

    public decimal Discount { get; set; }

    // Navigation properties

    public Customer Customer { get; set; } = null!;

    public CouponUsage? CouponUsage { get; set; }

    public ICollection<OrderItem> Items { get; set; } = [];
}