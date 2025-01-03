using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.OrderProcessing.Data;

internal class OrderProcessingDbContext : DbContext
{
  public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options) : base(options)
  {
  }

  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("OrderProcessing");

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(modelBuilder);
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>()
      .HavePrecision(18, 6);
  }
}
