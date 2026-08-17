using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;

namespace MiniShop.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderItem => orderItem.Id);

        builder.HasOne(orderItem => orderItem.Order)
        .WithMany(order => order.Items)
        .HasForeignKey(orderItem => orderItem.OrderId)
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();

        builder.HasOne(orderItem => orderItem.Product)
        .WithMany(product => product.OrderItems)
        .HasForeignKey(orderItem => orderItem.ProductId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired();

        builder.Property(x => x.UnitPrice).HasPrecision(10, 2);
    }
}