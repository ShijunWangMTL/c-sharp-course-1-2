using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pizza_FinalProject.Domain
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(10)]
        public string Phone { get; set; }

        // Client and Payment: one-to-one. One client has one payment in file.
        public virtual Payment Payment { get; set; }

        // Client and Order: one-to-many. One client can have more than one orders.
        public ICollection<Order> OrderCollection { get; set; }

    }
}
