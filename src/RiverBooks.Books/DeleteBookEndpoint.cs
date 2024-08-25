using FastEndpoints;

namespace RiverBooks.Books;

public record UpdateBookPriceRequest(Guid Id, decimal Price);
internal class UpdateBookPriceEndpoint(IBookService bookService) : Endpoint<UpdateBookPriceRequest, BookDto>
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
internal class DeleteBookEndpoint(IBookService bookService) : Endpoint<DeleteBookRequest>
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Delete("/books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteBookRequest request, CancellationToken ct)
  {
    await _bookService.DeleteBookAsync(request.Id);
    await SendNoContentAsync();
  }
}
