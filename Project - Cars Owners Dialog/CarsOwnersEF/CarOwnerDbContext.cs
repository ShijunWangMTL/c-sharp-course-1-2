using CarsOwnersEF.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsOwnersEF
{
    class CarOwnerDbContext : DbContext
    {
        // set the database location into the project folder
        const string DbName = "carOwnerDatabase.mdf";
        static string DbPath = Path.Combine(Environment.CurrentDirectory, DbName);
        static string connectionStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DbPath};Integrated Security=True;Connect Timeout=30";

        public CarOwnerDbContext() : base(connectionStr)
        {
        }

        // create entities
        public DbSet<CarOwner> carOwners { get; set; }
        public DbSet<Car> cars { get; set; }


    }
}
