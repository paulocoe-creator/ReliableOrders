using ReliableOrders.Domain.Entities;

namespace ReliableOrders.Application.Abstractions;

public interface IOrderRepository
{
    Task AddAsync(
        Order order,
        CancellationToken cancellationToken = default);
    
    Task<Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);    
}