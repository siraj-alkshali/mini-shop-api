using MiniShop.Domain.enums;

namespace MiniShop.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public Role Role { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation properties

    public Customer? Customer { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

}