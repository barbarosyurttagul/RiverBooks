namespace RiverBooks.Books;

internal class BookService : IBookService
{
  private readonly IBookRepository _bookRepository;

  public BookService(IBookRepository bookRepository)
  {
    _bookRepository = bookRepository;
  }

  public async Task CreateBookAsync(BookDto newBook)
  {
    var bookToBeCreated = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);
    await _bookRepository.AddAsync(bookToBeCreated);
    await _bookRepository.SaveChangesAsync();
  }

  public async Task DeleteBookAsync(Guid bookId)
  {
    var book = await _bookRepository.GetByIdAsync(bookId);
    if (book is not null)
    {
      await _bookRepository.DeleteAsync(book);
      await _bookRepository.SaveChangesAsync();
    }
  }

  public async Task<BookDto> GetByBookIdAsync(Guid bookId)
  {
    var book = await _bookRepository.GetByIdAsync(bookId);

    // TODO: Handle Not found case
    return new BookDto(book!.Id, book.Title, book.Author, book.Price);
  }

  public async Task<List<BookDto>> ListBooksAsync()
  {
    var books = (await _bookRepository.ListAsync())
      .Select(book => new BookDto(book.Id, book.Title, book.Author, book.Price))
      .ToList();

    return books;
  }

  public async Task UpdateBookPriceAsync(Guid bookId, decimal newPrice)
  {
    // validate the price

    var book = await _bookRepository.GetByIdAsync(bookId);

    // TODO: Handle Not found case

    book!.UpdatePrice(newPrice);
    await _bookRepository.SaveChangesAsync();
  }
}
