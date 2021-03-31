using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pizza_FinalProject.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        [EnumDataType(typeof(PaymentMethodEnum))]
        public PaymentMethodEnum PaymentMethod { get; set; }

        public enum PaymentMethodEnum { Credit_card = 1, Debit_card = 2, Cash = 3 }

        // if Cash is selected, the 3 fields below should be null
        public string CardHolderName { get; set; }

        public string CardNumber { get; set; }

        public DateTime? Expiration { get; set; }

        // Client and Payment: one-to-one. One client has one payment in file.
        public virtual Client Client { set; get; }
    }
}
