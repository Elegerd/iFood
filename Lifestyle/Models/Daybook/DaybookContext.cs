using Lifestyle.Models.Daybook;
using System.Data.Entity;

namespace Lifestyle.Models
{
    public class DaybookContext : DbContext
    {
        public DbSet<DaybookProduct> Daybooks { get; set; }
    }
}