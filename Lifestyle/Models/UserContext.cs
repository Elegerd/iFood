using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lifestyle.Models
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("Database1")
        { }

        public DbSet<User> Users { get; set; }
    }
}