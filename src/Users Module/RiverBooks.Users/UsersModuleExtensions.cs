using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{
  public static IServiceCollection AddUserModuleServices(
      this IServiceCollection services,
      IHostEnvironment env,
      ILogger logger)
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

    services.AddDbContext<UsersDbContext>(options =>
        options.UseSqlServer(connectionString));

    services.AddIdentityCore<ApplicationUser>()
      .AddEntityFrameworkStores<UsersDbContext>();

    logger.Information("{Module} module services registered", "Users");
    return services;
  }
}
