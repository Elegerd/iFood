using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Lifestyle.Models
{
    public class DefaultProduct
    {
        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Калории")]
        public int Calories { get; set; }

        public double Fiber { get; set; }
        public double Iron { get; set; }
        public double Calcium { get; set; }
        public double VitA { get; set; }
        public double VitC { get; set; }
        public double VitB12 { get; set; }
        public double Folate { get; set; }
    }
}