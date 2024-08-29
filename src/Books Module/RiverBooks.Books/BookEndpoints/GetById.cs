using FastEndpoints;

namespace RiverBooks.Books.BookEndpoints;

internal class GetById(IBookService bookService) : Endpoint<GetBookByIdRequest, BookDto>
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Get("/books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
  {
    var book = await _bookService.GetByBookIdAsync(req.Id);

    if (book is null)
    {
      await SendNotFoundAsync();
      return;
    }

    await SendAsync(book);
  }
}
