using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lifestyle.Models.Daybook
{
    public class DaybookProduct
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Gram { get; set; }
        public bool Custom { get; set; }
        public int ProductId { get; set; }
    }
}