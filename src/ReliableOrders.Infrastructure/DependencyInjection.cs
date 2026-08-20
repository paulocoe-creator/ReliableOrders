using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReliableOrders.Application.Abstractions;
using ReliableOrders.Infrastructure.Persistence;
using ReliableOrders.Infrastructure.Persistence.Repositories;


namespace ReliableOrders.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")
        ?? throw new InvalidOperationException(
            "Connection string 'Postgres' was not found.");
        
        services.AddDbContext<ReliableOrdersDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IUnitOfWork>(provider => 
            provider.GetRequiredService<ReliableOrdersDbContext>());

        return services;
    }
}