using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReliableOrders.Domain.Entities;

namespace ReliableOrders.Infrastructure.Persistence.Configurations;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.ProductName)
            .HasColumnName("product_name")
            .IsRequired();

        builder.Property(item => item.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.OwnsOne(
            item => item.UnitPrice,
            money =>
            {
                money.Property(value => value.Amount)
                    .HasColumnName("unit_price_amount");
                
                money.Property(value => value.Currency)
                    .HasColumnName("unit_price_currency")
                    .HasMaxLength(3);
            });
            
        builder.Ignore(item => item.TotalPrice);
    }
}