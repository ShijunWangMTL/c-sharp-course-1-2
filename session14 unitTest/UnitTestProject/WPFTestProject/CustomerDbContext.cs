using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestProject.Domain;

namespace WPFTestProject
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> customers { get; set; }
    }
}
