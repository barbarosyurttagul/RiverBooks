namespace RiverBooks.Books;

internal interface IBookRepository : IReadOnlyBookRepository
{
  Task AddAsync(Book book);
  Task SaveChangesAsync(Book book);
  Task DeleteAsync(Guid id);
}
