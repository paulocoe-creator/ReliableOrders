using ReliableOrders.Application.Abstractions;
using ReliableOrders.Domain.Entities;

namespace ReliableOrders.Application.Orders;

public sealed class CreateOrder
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrder(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(
        string customerEmail,
        string currency,
        CancellationToken cancellationToken = default)
    {
        var order = new Order(
            Guid.NewGuid(),
            customerEmail,
            currency);

        await _orderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }    
}