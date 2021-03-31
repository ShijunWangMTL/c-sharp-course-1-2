using PersonPassportDialog.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonPassportDialog
{
    public class PersonPassportDbContext : DbContext
    {
        const string DbName = "PersonPassportDb.mdf";
        static string DbPath = Path.Combine(Environment.CurrentDirectory, DbName);
        public PersonPassportDbContext() : base($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DbPath};Integrated Security=True;Connect Timeout=30")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                        .HasOptional(p => p.Passport)
                        .WithRequired(passport => passport.Person);

        }

        public virtual DbSet<Person> persons { get; set; }
        public virtual DbSet<Passport> passports { get; set; }
    }
}
