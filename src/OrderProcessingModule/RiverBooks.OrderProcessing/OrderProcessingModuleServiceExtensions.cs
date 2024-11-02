using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiverBooks.OrderProcessing.Data;
using Serilog;

namespace RiverBooks.OrderProcessing;

public static class OrderProcessingModuleServiceExtensions
{
  public static IServiceCollection AddOrderProcessingModuleServices(
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

    services.AddDbContext<OrderProcessingDbContext>(options =>
        options.UseSqlServer(connectionString));

    services.AddScoped<IOrderRepository, EfOrderRepository>();

    // if using MediatR in this module, add any assmeblies that contain handlers to the module
    mediatRAssemblies.Add(typeof(OrderProcessingModuleServiceExtensions).Assembly);

    logger.Information("{Module} module services registered", "OrderProcessing");
    return services;
  }
}
