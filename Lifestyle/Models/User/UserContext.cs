using System.Data.Entity;

namespace Lifestyle.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}