using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonPassportDialog.Domain
{
    public class Passport
    {
        public int PassportId { get; set; }

        [Required]
        [StringLength(9)]
        [Index(IsUnique = true)]
        public string PassportNo { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        public virtual Person Person { get; set; }
    }
}
