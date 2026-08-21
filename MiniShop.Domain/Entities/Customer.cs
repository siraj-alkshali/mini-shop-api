namespace MiniShop.Domain.Entities;

public class Customer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // Navigation properties

    public User User { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = [];

    public ICollection<CouponUsage> CouponUsages { get; set; } = [];
}