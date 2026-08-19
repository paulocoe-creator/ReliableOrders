using Microsoft.EntityFrameworkCore;
using ReliableOrders.Application.Abstractions;
using ReliableOrders.Domain.Entities;

namespace ReliableOrders.Infrastructure.Persistence.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly ReliableOrdersDbContext _dbContext;

    public OrderRepository(ReliableOrdersDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(order, cancellationToken);        
    }

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Orders
            .Include(order => order.Items)
            .FirstOrDefaultAsync(
                order => order.Id == id,
                cancellationToken
            );
    }
}