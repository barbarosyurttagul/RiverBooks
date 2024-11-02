using Microsoft.EntityFrameworkCore;

namespace RiverBooks.OrderProcessing.Data;

internal class EfOrderRepository : IOrderRepository
{
  private readonly OrderProcessingDbContext _dbContext;
  public EfOrderRepository(OrderProcessingDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  public async Task AddAsync(Order order)
  {
    await _dbContext.Orders.AddAsync(order);
  }

  public Task<List<Order>> ListAsync()
  {
    return _dbContext.Orders
    .Include(o => o.OrderItems)
    .ToListAsync();
  }

  public Task SaveChangesAsync()
  {
    return _dbContext.SaveChangesAsync();
  }
}
