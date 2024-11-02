using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiverBooks.Books.Data;
using Serilog;

namespace RiverBooks.Books;

public static class BookServiceExtensions
{
  public static IServiceCollection AddBookModuleServices(
    this IServiceCollection services,
    IHostEnvironment env,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies)
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

    // if using MediatR in this module, add any assmeblies that contain handlers to the module
    mediatRAssemblies.Add(typeof(BookServiceExtensions).Assembly);

    logger.Information("{Module} module services registered", "Books");
    return services;
  }
}
