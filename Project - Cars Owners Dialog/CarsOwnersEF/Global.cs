using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsOwnersEF
{
    static class Global
    {
        // singleton
        public static CarOwnerDbContext ctx = new CarOwnerDbContext();
    }
}
