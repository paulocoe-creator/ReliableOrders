using ReliableOrders.Application.Orders;

namespace ReliableOrders.Application.Tests;

public class CreateOrderTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCreateAndPersistOrder()
    {
        // Arrange
        var repository = new FakeOrderRepository();
        var fakeUnitOfWork =  new FakeUnitOfWork();
        var useCase = new CreateOrder(repository, fakeUnitOfWork);

        // Act
        var orderId = await useCase.ExecuteAsync("customer@example.com", "USD");

        var savedOrder = await repository.GetByIdAsync(orderId);

        // Assert

        Assert.NotEqual(Guid.Empty, orderId);
        Assert.NotNull(savedOrder);
        Assert.Equal("customer@example.com", savedOrder.CustomerEmail);
        Assert.Equal("USD", savedOrder.Currency);
    }
}