namespace RiverBooks.Books;

internal class BookService : IBookService
{
    public List<BookDto> ListBooks()
    {
        return [
            new BookDto(Guid.NewGuid(), "The Great Gatsby", "F. Scott Fitzgerald"),
            new BookDto(Guid.NewGuid(), "To Kill a Mockingbird", "Harper Lee"),
            new BookDto(Guid.NewGuid(), "1984", "George Orwell")
        ];
    }
}