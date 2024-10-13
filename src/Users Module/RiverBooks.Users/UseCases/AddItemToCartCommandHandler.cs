using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository;

  public AddItemToCartCommandHandler(IApplicationUserRepository applicationUserRepository)
  {
    _userRepository = applicationUserRepository;
  }
  public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    // TODO: Get description and price from Books Module
    var newCartItem = new CartItem(request.BookId,
    "description",
    request.Quantity,
    1.00m);

    user.AddItemToCart(newCartItem);

    await _userRepository.SaveChangesAsync();

    return Result.Success();
  }
}
