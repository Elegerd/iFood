using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Lifestyle.Models.Product;

namespace Lifestyle.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<DefaultProduct> DefaultProducts { get; set; }
    }
}