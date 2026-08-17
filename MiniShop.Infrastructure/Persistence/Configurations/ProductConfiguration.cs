using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;

namespace MiniShop.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name).HasMaxLength(100).IsRequired();

        builder.Property(product => product.Price).HasPrecision(10, 2);

        builder.HasData(new Product
        {
            Id = 1,
            Name = "Laptop",
            StockQuantity = 10,
            Price = 999.99m
        });
    }
}