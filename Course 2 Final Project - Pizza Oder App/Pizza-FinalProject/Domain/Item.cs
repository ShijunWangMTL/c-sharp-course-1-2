using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_FinalProject.Domain
{
    public class Item
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public double PT { get; set; }

        // Order and Item: zero-to-many
        public virtual Order Order { get; set; }

        // Item and Pizza: one-to-many. One pizza can exist in more than one Item table (for different orders)
        public int PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }

    }
}
