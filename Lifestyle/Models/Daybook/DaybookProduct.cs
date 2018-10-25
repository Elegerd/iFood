using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lifestyle.Models.Daybook
{
    public class DaybookProduct
    {
        public int Id { get; set; }
        public int UId { get; set; }
        public DefaultProduct AProduct { get; set; }
    }
}