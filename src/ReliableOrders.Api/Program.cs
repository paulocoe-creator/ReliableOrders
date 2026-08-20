using ReliableOrders.Api.Contracts;
using ReliableOrders.Application.Orders;
using ReliableOrders.Infrastructure;
using ReliableOrders.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/orders", async (
    CreateOrderRequest request,
    CreateOrder createOrder,
    CancellationToken cancellationToken) =>
{
    var orderId = await createOrder.ExecuteAsync(
        request.CustomerEmail,
        request.Currency,
        cancellationToken);

    return Results.Created($"/orders/{orderId}", new
    {
        Id = orderId
    });
}
);

app.Run();