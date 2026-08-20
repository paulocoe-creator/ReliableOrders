using Microsoft.Extensions.DependencyInjection;
using ReliableOrders.Application.Orders;
using ReliableOrders.Application.Orders;

namespace ReliableOrders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateOrder>();
        
        return services;
    }
}