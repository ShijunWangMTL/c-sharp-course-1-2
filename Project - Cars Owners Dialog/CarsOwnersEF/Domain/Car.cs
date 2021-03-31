using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsOwnersEF.Domain
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(50)]
        public string MakeModel { get; set; }

        [ForeignKey("CarOwner")]
        public int Id { get; set; }

        // Single Field - always eagerly loaded
        public virtual CarOwner CarOwner { get; set; }

        public override string ToString()
        {
            return $"{Id}: {MakeModel}";
        }

    }
}
