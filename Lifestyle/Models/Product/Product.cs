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
        public int Fats { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }

        public double Fiber { get; set; }
        public double Iron { get; set; }
        public double Calcium { get; set; }
        public double VitA { get; set; }
        public double VitC { get; set; }
        public double VitB12 { get; set; }
        public double Folate { get; set; }
    }

    public class CustomProduct
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Калории")]
        public int Calories { get; set; }
        public int Fats { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }

        public double Fiber { get; set; }
        public double Iron { get; set; }
        public double Calcium { get; set; }
        public double VitA { get; set; }
        public double VitC { get; set; }
        public double VitB12 { get; set; }
        public double Folate { get; set; }
    }

    public class ProductModel {
        public DefaultProduct DefaultProducts { get; set; }
        public CustomProduct CustomProducts { get; set; }
    }
}