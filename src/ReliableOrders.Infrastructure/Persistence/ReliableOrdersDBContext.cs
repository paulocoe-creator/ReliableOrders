using Microsoft.EntityFrameworkCore;
using ReliableOrders.Application.Abstractions;
using ReliableOrders.Domain.Entities;
using ReliableOrders.Infrastructure.Persistence.Configurations;

namespace ReliableOrders.Infrastructure.Persistence;

public sealed class ReliableOrdersDbContext(DbContextOptions<ReliableOrdersDbContext> options) 
: DbContext(options), IUnitOfWork
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReliableOrdersDbContext).Assembly);

        base.OnModelCreating(modelBuilder);        
    }
}