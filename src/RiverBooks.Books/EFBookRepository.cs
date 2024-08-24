
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal class EFBookRepository : IBookRepository
{
  private readonly BookDbContext _dbContext;
  public EFBookRepository(BookDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  public Task AddAsync(Book book)
  {
    _dbContext.Add(book);
    return Task.CompletedTask;
  }

  public Task DeleteAsync(Guid id)
  {
    _dbContext.Remove(id);
    return Task.CompletedTask;
  }

  public async Task<Book?> GetByIdAsync(Guid id)
  {
    return await _dbContext.Books.FindAsync(id);
  }

  public async Task<List<Book>> ListAsync()
  {
    return await _dbContext.Books.ToListAsync();
  }

  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}
