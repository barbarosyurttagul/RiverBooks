namespace RiverBooks.Books;

internal interface IBookRepository : IReadOnlyBookRepository
{
  Task AddAsync(Book book);
  Task SaveChangesAsync();
  Task DeleteAsync(Guid id);
}
