using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

public static class BookServiceExtensions
{
  public static IServiceCollection AddBookServices(
    this IServiceCollection services)
  {
    var connectionString = Environment.GetEnvironmentVariable("RIVERBOOK_CONNECTION");
    services.AddDbContext<BookDbContext>(options =>
        options.UseSqlServer(connectionString));
    services.AddScoped<IBookRepository, EFBookRepository>();
    services.AddScoped<IBookService, BookService>();
    return services;
  }
}
