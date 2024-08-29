using FastEndpoints;
using FluentValidation;

namespace RiverBooks.Books.BookEndpoints;

public record UpdateBookPriceRequest(Guid Id, decimal Price);

public class UpdateBookPriceRequestValidator : Validator<UpdateBookPriceRequest>
{
  public UpdateBookPriceRequestValidator()
  {
    RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage("Id is required.");

    RuleFor(x => x.Price)
        .GreaterThan(0)
        .WithMessage("Price must be greater than 0.");
  }
}
