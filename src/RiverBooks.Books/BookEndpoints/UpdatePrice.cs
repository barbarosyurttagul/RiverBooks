using FastEndpoints;

namespace RiverBooks.Books.BookEndpoints;

internal class UpdatePrice(IBookService bookService) : Endpoint<UpdateBookPriceRequest, BookDto>
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Post("/books/{Id}/priceHistory");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UpdateBookPriceRequest request, CancellationToken ct)
  {
    await _bookService.UpdateBookPriceAsync(request.Id, request.Price);
    var updatedBook = await _bookService.GetByBookIdAsync(request.Id);
    await SendAsync(updatedBook);
  }
}
