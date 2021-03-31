/*using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_FinalProject.Domain
{
    public class Topping
    {
        public int Id { get; set; }

        [EnumDataType(typeof(ToppingNameEnum))]
        public ToppingNameEnum ToppingName { get; set; }

        public enum ToppingNameEnum { Cheese = 1, All_Dressed = 2, Hawaiian = 3, Salami = 4, Mixed = 5, Vegetarian = 6 }

        public double price { get; set; }


        // Item and Topping: one-to-many. One topping can exist in more than one Item table (for different orders)
        public ICollection<Item> ItemsContainTopping { get; set; }
    }
}
*/