using FastEndpoints;

namespace RiverBooks.Books.BookEndpoints;

internal class Delete(IBookService bookService) : Endpoint<DeleteBookRequest>
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