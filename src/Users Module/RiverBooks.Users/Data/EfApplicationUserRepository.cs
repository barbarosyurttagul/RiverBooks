using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Data;

namespace RiverBooks.Users;

public class EfApplicationUserRepository : IApplicationUserRepository
{
  private readonly UsersDbContext _usersDbContext;

  public EfApplicationUserRepository(UsersDbContext usersDbContext)
  {
    _usersDbContext = usersDbContext;
  }
  public Task<ApplicationUser> GetUserWithCartByEmailAsync(string emailAddress)
  {
    return _usersDbContext.ApplicationUsers
      .Include(u => u.CartItems)
      .SingleAsync(u => u.Email == emailAddress);
  }

  public Task SaveChangesAsync()
  {
    return _usersDbContext.SaveChangesAsync();
  }
}
