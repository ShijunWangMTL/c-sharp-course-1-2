using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_FinalProject.Domain
{
   public class Pizza

    {
        public int Id { get; set; }

        [EnumDataType(typeof(PizzaNameEnum))]
        public PizzaNameEnum PizzaName { get; set; }

        public enum PizzaNameEnum { Cheese = 1, All_Dressed = 2, Hawaiian = 3, Salami = 4, Vegetarian = 5 }

        [NotMapped]
        public byte[] Photo { get; set; }

        public double price { get; set; }

        public PizzaSizeEnum Size { get; set; }
        public enum PizzaSizeEnum { Small = 1, Medium = 2, Large = 3 }

        // Item and Pizza: one-to-many. One pizza can exist in more than one Item table (for different orders)
        public ICollection<Item> ItemsContainPizza { get; set; }

        public void InsertPizzaTable()
        {          
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Cheese, Size = PizzaSizeEnum.Small, price = 9.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Cheese, Size = PizzaSizeEnum.Medium, price = 11.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Cheese, Size = PizzaSizeEnum.Large, price = 13.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.All_Dressed, Size = PizzaSizeEnum.Small, price = 9.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.All_Dressed, Size = PizzaSizeEnum.Medium, price = 11.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.All_Dressed, Size = PizzaSizeEnum.Large, price = 13.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Hawaiian, Size = PizzaSizeEnum.Small, price = 9.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Hawaiian, Size = PizzaSizeEnum.Medium, price = 11.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Hawaiian, Size = PizzaSizeEnum.Large, price = 13.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Salami, Size = PizzaSizeEnum.Small, price = 9.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Salami, Size = PizzaSizeEnum.Medium, price = 11.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Salami, Size = PizzaSizeEnum.Large, price = 13.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Vegetarian, Size = PizzaSizeEnum.Small, price = 9.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Vegetarian, Size = PizzaSizeEnum.Medium, price = 11.00 });
            Global.ctx.Pizzas.Add(new Pizza() { PizzaName = PizzaNameEnum.Vegetarian, Size = PizzaSizeEnum.Large, price = 13.00 });

            Global.ctx.SaveChanges();
        }
    }
}
