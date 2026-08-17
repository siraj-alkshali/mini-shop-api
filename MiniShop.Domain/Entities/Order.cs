using MiniShop.Domain.Enums;

namespace MiniShop.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    // public int CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public enOrderStatus Status { get; set; }

    // public decimal Subtotal { get; set; }

    public decimal ShippingCost { get; set; }

    public decimal Discount { get; set; }

    // Navigation properties

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}