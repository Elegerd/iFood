using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lifestyle.Models
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }


    public class Authentication
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}