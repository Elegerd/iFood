using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lifestyle.Models.Indicator
{
    public class IndicatorContext : DbContext
    {
        public DbSet<Indicator> Indicators { get; set; }
    }
}