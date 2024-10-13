namespace RiverBooks.Users;

public interface IApplicationUserRepository
{
  Task<ApplicationUser> GetUserWithCartByEmailAsync(string emailAddress);
  Task SaveChangesAsync();
}
