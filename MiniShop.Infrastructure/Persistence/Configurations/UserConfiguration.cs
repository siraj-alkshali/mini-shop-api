using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;

namespace MiniShop.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasIndex(user => user.Username).IsUnique();

        builder.Property(user => user.Username).HasMaxLength(50).IsRequired();

        builder.Property(user => user.PasswordHash).HasMaxLength(255).IsRequired();
    }
}