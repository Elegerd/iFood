using System.Data.Entity;

namespace Lifestyle.Models
{
    public class DefaultProductContext : DbContext
    {
        public DbSet<DefaultProduct> DefaultProducts { get; set; }
    }
}