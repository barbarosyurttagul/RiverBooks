using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.Users.UseCases.Checkout;

public class CheckoutCartCommandHandler : IRequestHandler<CheckoutCartCommand, Result<Guid>>
{
  private readonly IApplicationUserRepository _userRepository;
  private readonly IMediator _mediator;

  public CheckoutCartCommandHandler(IApplicationUserRepository userRepository, IMediator mediator)
  {
    _userRepository = userRepository;
    _mediator = mediator;
  }
  public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.emailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    var items = user.CartItems.Select(item =>
        new OrderItemDetails(item.BookId,
                             item.Quantity,
                             item.UnitPrice,
                             item.Description))
                             .ToList();

    var createOrderCommand = new CreateOrderCommand(Guid.Parse(user.Id),
                                                    request.ShippingAddressId,
                                                    request.BillingAddressId,
                                                    items);

    // TODO: Consider replacing with a message-based approach for perf reasons
    var result = await _mediator.Send(createOrderCommand);

    if (!result.IsSuccess)
    {
      // Change from a Result<OrderDetailsResponse> to Result<Guid> 
      return result.Map(x => x.OrderId);
    }

    user.ClearCart();
    await _userRepository.SaveChangesAsync();

    return Result.Success(result.Value.OrderId);
  }
}
