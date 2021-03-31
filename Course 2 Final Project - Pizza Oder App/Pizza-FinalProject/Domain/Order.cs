using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_FinalProject.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public double Total { get; set; }
        [NotMapped]
        public double TotalBeforeTax { get; set; }       
        public double GST { get; set; }
        public double QST { get; set; }
        public double Tip { get; set; }

        [EnumDataType(typeof(DeliverMethodEnum))]
        public DeliverMethodEnum DeliverMethod { get; set; }
        public enum DeliverMethodEnum { Pickup = 1, Delivery = 2 }

        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
        public enum StatusEnum { Confirmed = 1, Preparing = 2, Ready_to_pickup = 3, Delivering = 4, Complete = 5, Cancelled = 6 }

        // Client and Order: one-to-many. One client can have more than one orders.
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        // Order and Item: one-to-many. One order can contain plural items(each item with quantity).
        public ICollection<Item> ItemsInOrder { get; set; }

    }
}
