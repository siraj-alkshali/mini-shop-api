using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;

namespace MiniShop.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);

        builder.HasOne(customer => customer.User)
        .WithOne(user => user.Customer)
        .HasForeignKey<Customer>(customer => customer.UserId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();

        builder.HasIndex(customer => customer.Email).IsUnique();

        builder.Property(customer => customer.Name).HasMaxLength(100).IsRequired();

        builder.Property(customer => customer.Email).HasMaxLength(254).IsRequired();
    }
}