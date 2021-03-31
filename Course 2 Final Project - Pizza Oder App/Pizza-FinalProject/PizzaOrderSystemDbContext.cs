using Pizza_FinalProject.Domain;
using System;
using System.Data.Entity;
using System.IO;

namespace Pizza_FinalProject
{
    public class PizzaOrderSystemDbContext : DbContext
    {
        const string DbName = "pizzaordersystemdb.mdf";
        static string DbPath = Path.Combine(Environment.CurrentDirectory, DbName);
        public PizzaOrderSystemDbContext() : base($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DbPath};Integrated Security=True;Connect Timeout=30")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                        .HasRequired(c => c.Payment)
                        .WithRequiredPrincipal(p => p.Client);

            modelBuilder.Entity<Client>()
                        .HasIndex(c => c.Phone)
                        .IsUnique();

            modelBuilder.Entity<Item>()
                        .HasOptional<Order>(i => i.Order)
                        .WithMany(o => o.ItemsInOrder);

            modelBuilder.Entity<Item>()
                        .HasRequired<Pizza>(i => i.Pizza)
                        .WithMany(p => p.ItemsContainPizza)
                        .HasForeignKey<int>(i => i.PizzaId);


            modelBuilder.Entity<Order>()
                        .HasRequired<Client>(o => o.Client)
                        .WithMany(c => c.OrderCollection)
                        .HasForeignKey<int>(o => o.ClientId);

/*            modelBuilder.Entity<Order>()
                        .Property(o => o.Tip).HasColumnType("decimal(5,2)");
            modelBuilder.Entity<Order>()
                        .Property(o => o.Total).HasColumnType("decimal(10,2)");*/
        }




        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }


    }
}