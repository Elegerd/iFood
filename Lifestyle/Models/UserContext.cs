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
            base()
        { }

        public DbSet<User> Users { get; set; }
    }
}