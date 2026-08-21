namespace MiniShop.Domain.Entities;

public class CouponUsage
{
    public int Id { get; set; }

    public int CouponId { get; set; }

    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    public DateTime UsedAt { get; set; }

    // Navigation properties

    public Customer Customer { get; set; } = null!;

    public Coupon Coupon { get; set; } = null!;

    public Order Order { get; set; } = null!;

}