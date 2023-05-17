using Api.Models;
using api_back.Models;
using Microsoft.EntityFrameworkCore;

namespace api_back.Users.Data
{
    public class UserApiDbContext : DbContext
    {
        public UserApiDbContext()
        {
        }
        public UserApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
