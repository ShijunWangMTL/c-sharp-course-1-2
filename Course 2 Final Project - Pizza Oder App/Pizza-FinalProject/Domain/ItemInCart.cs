using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_FinalProject.Domain
{
    public class ItemInCart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double PT { get; set; }
        public int PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
