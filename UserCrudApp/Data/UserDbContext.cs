using Microsoft.EntityFrameworkCore;
using UserCrudApp.Models;

namespace UserCrudApp.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}
