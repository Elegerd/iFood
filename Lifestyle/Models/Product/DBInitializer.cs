using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Lifestyle.Models.Product
{
    public class DBInitializer : DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            context.DefaultProducts.Add(new DefaultProduct() { Name = "Гречневая каша", Calories = 92, Fats = 5, Protein = 11, Carbs = 76, Fiber = 2.7, Iron = 0.8, Calcium = 7.0, VitA = 0, VitC = 0, VitB12 = 0, Folate = 14.0 });
            context.DefaultProducts.Add(new DefaultProduct() { Name = "Яблоко", Calories = 52, Fats = 1, Protein = 1, Carbs = 50, Fiber = 2.4, Iron = 0.1, Calcium = 6.0, VitA = 54, VitC = 4.6, VitB12 = 0, Folate = 3.0 });
            context.DefaultProducts.Add(new DefaultProduct() { Name = "Банан", Calories = 89, Fats = 3, Protein = 4, Carbs = 82, Fiber = 2.6, Iron = 0.3, Calcium = 5.0, VitA = 64, VitC = 8.7, VitB12 = 0, Folate = 20.0 });
            context.DefaultProducts.Add(new DefaultProduct() { Name = "Морковь", Calories = 41, Fats = 2, Protein = 3, Carbs = 36, Fiber = 2.8, Iron = 0.3, Calcium = 33.0, VitA = 16706, VitC = 5.9, VitB12 = 0, Folate = 19.0 });
            context.DefaultProducts.Add(new DefaultProduct() { Name = "Молоко", Calories = 61, Fats = 29, Protein = 13, Carbs = 19, Fiber = 0, Iron = 0, Calcium = 113.0, VitA = 162, VitC = 0, VitB12 = 0.5, Folate = 5.0 });
            base.Seed(context);
        }
    }
}