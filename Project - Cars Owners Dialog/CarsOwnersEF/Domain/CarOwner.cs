using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsOwnersEF.Domain
{
    public class CarOwner
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        //NotMapped is having a field in the class without save it as a column in the table
        [NotMapped]
        public int carsNumber { get => CarsInGarage.Count; }

        public ICollection<Car> CarsInGarage { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }

    }
}
