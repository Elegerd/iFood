using System.Data.Entity;

namespace Lifestyle.Models
{
    public class CustomProductContext : DbContext
    {
        public DbSet<CustomProduct> CustomProducts { get; set; }
    }
}