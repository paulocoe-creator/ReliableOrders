using ReliableOrders.Domain.Common;
using ReliableOrders.Domain.ValueObjects;

namespace ReliableOrders.Domain.Entities;

public class Order : Entity
{
    private readonly List<OrderItem> _items = []; 
    
    public DateTime CreatedAtUtc { get; private set; }

    public string CustomerEmail { get; private set; }

    public Money TotalAmount => new(_items.Sum(item => item.TotalPrice.Amount), Currency);

    public string Currency { get; private set; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Order(Guid id, string customerEmail, string currency) : base(id)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency is required", nameof(currency));
        }

        if (string.IsNullOrEmpty(customerEmail))
        {
            throw new ArgumentException("Customer email is required.", nameof(customerEmail));
        }        

        Id = id;
        CustomerEmail = customerEmail;
        Currency = currency;
        CreatedAtUtc = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException(
                $"Only pending orders can be confirmed. Current status: {Status}");
        }

        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if(Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("Order is already cancelled.");
        }

        if (Status == OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Confirmed orders cannot be cancelled.");
        }

        Status = OrderStatus.Cancelled;
    }

    public void AddItem(OrderItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Items can only be added to pending orders.");
        }

        if (_items.Any(existing => existing.Id == item.Id))
        {
            throw new InvalidOperationException($"Order item with id '{item.Id}' already exists.");
        }

        if (item.UnitPrice.Currency != Currency)
        {
            throw new InvalidOperationException($"Cannot add an item in {item.UnitPrice.Currency} " +
            $"to an order in {Currency}");
        }
        _items.Add(item);
    }
}
