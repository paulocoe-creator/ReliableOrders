using ReliableOrders.Application.Abstractions;

namespace ReliableOrders.Application.Tests;

public sealed class FakeUnitOfWork : IUnitOfWork
{
    public int SaveChangesCallCount {get; private set;}

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        SaveChangesCallCount++;

        return Task.FromResult(1);
    }
}