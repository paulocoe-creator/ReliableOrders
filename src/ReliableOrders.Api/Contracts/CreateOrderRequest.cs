namespace ReliableOrders.Api.Contracts;

public sealed record CreateOrderRequest(
    string CustomerEmail,
    string Currency);