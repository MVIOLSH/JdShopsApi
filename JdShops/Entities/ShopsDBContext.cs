using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops.Entities
{
    public class ShopsDBContext : DbContext
    {
        private string _connectionString = 
            "Server=(localdb)\\mssqllocaldb;Database=JDSopsDbNew;Trusted_Connection=True;";
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AdditionalAddress> AdditionalAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shops>()
            .Property(r => r.ShopNumber)
            .IsRequired();

            modelBuilder.Entity<Address>()
           .Property(r => r.ShopNumber)
           .IsRequired();

            modelBuilder.Entity<AdditionalAddress>()
                .Property(r => r.ShopNumber)
                .IsRequired();


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
