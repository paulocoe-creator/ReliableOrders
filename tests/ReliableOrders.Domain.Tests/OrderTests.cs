using ReliableOrders.Domain.Entities;
using ReliableOrders.Domain.ValueObjects;

namespace ReliableOrders.Domain.Tests;

public class OrderTests
{
    [Fact]
    public void Constructor_ShouldCreateOrderWithPendingStatus()
    {
        // Arrange & Act
        var order = new Order(Guid.NewGuid(), "customer@example.com", "USD");

        // Assert
        Assert.Equal(OrderStatus.Pending, order.Status);
    }

    [Fact]
    public void Confirm_ShouldChangeStatusToConfirmed()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "customer@example.com", "USD");

        // Act
        order.Confirm();

        // Assert
        Assert.Equal(OrderStatus.Confirmed, order.Status);
    }

    [Fact]
    public void Confirm_WhenOrderIsAlreadyConfirmed_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "customer@example.com", "USD");

        order.Confirm();

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => order.Confirm());

        // Assert
        Assert.Contains("Only pending orders can be confirmed", exception.Message);

    }

    [Fact]
    public void AddItem_ShouldAddItemAndUpdateTotalAmount()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "customer@example.com", "USD");

        var item = new OrderItem(Guid.NewGuid(), "Mechanical Keyboard", 2, new Money(100, "USD"));

        // Act
        order.AddItem(item);

        // Assert
        Assert.Single(order.Items);
        Assert.Equal(200, order.TotalAmount.Amount);
        Assert.Equal("USD", order.TotalAmount.Currency);
    }

    [Fact]
    public void AddItem_WithDifferentCurrency_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "customer@example.com", "EUR");

        var orderItem = new OrderItem(Guid.NewGuid(), "Cupboard", 3, new Money(14.99m, "USD"));

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => order.AddItem(orderItem));

        Assert.Contains("Cannot add an item in USD", exception.Message);
    }

    [Fact]
    public void AddItem_WithDuplicateItemId_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "customer@example.com", "USD");

        var orderItemId = Guid.NewGuid();

        var orderItem1 = new OrderItem(orderItemId, "Cupboard", 3, new Money(14.99m, "USD"));
        var orderItem2 = new OrderItem(orderItemId, "Table", 2, new Money(54.99m, "USD"));

        order.AddItem(orderItem1);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => order.AddItem(orderItem2));

        Assert.Contains($"Order item with id '{orderItemId}' already exists.", exception.Message);    
    }
}