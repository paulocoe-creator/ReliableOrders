using ReliableOrders.Domain.Common;
using ReliableOrders.Domain.ValueObjects;

namespace ReliableOrders.Domain.Entities;

public class OrderItem : Entity
{
    public string ProductName { get; private set; } 

    public int Quantity { get; private set; }

    public Money UnitPrice { get; private set; }

    public Money TotalPrice => new(Quantity * UnitPrice.Amount, UnitPrice.Currency);

    public OrderItem(Guid id, string productName, int quantity, Money unitPrice)
    : base(id)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        ArgumentNullException.ThrowIfNull(unitPrice);

        Id = id;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

}