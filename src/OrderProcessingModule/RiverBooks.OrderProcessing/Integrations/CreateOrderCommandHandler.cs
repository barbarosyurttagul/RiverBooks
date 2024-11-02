using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.OrderProcessing.Data;
using Microsoft.Extensions.Logging;

namespace RiverBooks.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,
                                                          Result<OrderDetailsResponse>>
{
  private readonly IOrderRepository _orderRepository;
  private readonly ILogger<CreateOrderCommandHandler> _logger;

  public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                   ILogger<CreateOrderCommandHandler> logger)
  {
    _orderRepository = orderRepository;
    _logger = logger;
  }
  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    var items = request.OrderItems.Select(orderItem => new OrderItem(
      orderItem.BookId,
      orderItem.UnitPrice,
      orderItem.Quantity,
      orderItem.Description
    ));

    var shippingAddress = new Address("123 Main", "", "Kent", "OH", "44444", "USA");
    var billingAddress = shippingAddress;
    var newOrder = Order.Factory.Create(request.UserId,
                                    shippingAddress,
                                    billingAddress,
                                    items);

    await _orderRepository.AddAsync(newOrder);
    await _orderRepository.SaveChangesAsync();

    _logger.LogInformation("New Order Created {OrderId}", newOrder.Id);

    return new OrderDetailsResponse(newOrder.Id);

  }
}
