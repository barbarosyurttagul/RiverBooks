using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Data;

namespace RiverBooks.OrderProcessing.Endpoints;

public class ListOrdersForUserResponse
{
  public List<OrderSummary> Orders { get; set; } = new();
}

internal class ListOrdersForUserQueryHandler :
  IRequestHandler<ListOrdersForUserQuery,
  Result<List<OrderSummary>>>
{
  private readonly IOrderRepository _orderRepository;

  public ListOrdersForUserQueryHandler(IOrderRepository orderRepository)
  {
    _orderRepository = orderRepository;
  }
  public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
  {
    // look up userId for email address

    // TODO: filter by User
    var orders = await _orderRepository.ListAsync();

    var summaries = orders.Select(o => new OrderSummary
    {
      OrderId = o.Id,
      UserId = o.UserId,
      DateCreated = o.DateCreated,
      Total = o.OrderItems.Sum(oi => oi.UnitPrice) // need to .Include OrderItems
    }).ToList();

    return summaries;
  }
}
