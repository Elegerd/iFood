using Lifestyle.Models.Daybook;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lifestyle.Models.DaybookContext
{
    public class DaybookContext : DbContext
    {
        public DbSet<DaybookProduct> Daybooks { get; set; }
    }
}