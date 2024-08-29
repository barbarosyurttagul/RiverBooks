using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiverBooks.Books.Data;

namespace RiverBooks.Books;

public static class BookServiceExtensions
{
  public static IServiceCollection AddBookServices(
    this IServiceCollection services, IHostEnvironment env)
  {
    string connectionString;
    if (env.EnvironmentName == "Testing")
    {
      connectionString = Environment.GetEnvironmentVariable("RIVERBOOK_CONNECTION_TESTING")!;
    }
    else
    {
      connectionString = Environment.GetEnvironmentVariable("RIVERBOOK_CONNECTION")!;
    }

    services.AddDbContext<BookDbContext>(options =>
        options.UseSqlServer(connectionString));
    services.AddScoped<IBookRepository, EFBookRepository>();
    services.AddScoped<IBookService, BookService>();
    return services;
  }
}
