using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;

namespace MiniShop.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

        builder.Property(x => x.Status).HasConversion<string>();

        builder.Property(order => order.ShippingCost).HasPrecision(10, 2);

        builder.Property(order => order.Discount).HasPrecision(10, 2);
    }
}