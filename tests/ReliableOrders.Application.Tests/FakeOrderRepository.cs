using ReliableOrders.Application.Abstractions;
using ReliableOrders.Domain.Entities;

namespace ReliableOrders.Application.Tests;

public sealed class FakeOrderRepository : IOrderRepository
{

    private readonly Dictionary<Guid, Order> _orders = [];

    public Task AddAsync(Order order, CancellationToken cancellationToke = default)
    {
        _orders[order.Id] = order;

        return Task.CompletedTask;
    }

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!_orders.TryGetValue(id, out var order))
        {
            return Task.FromResult<Order?>(null);
        }

        return Task.FromResult<Order?>(order);
    }
}