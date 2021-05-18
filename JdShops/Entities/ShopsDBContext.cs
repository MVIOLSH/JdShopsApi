using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdShops.Entities
{
    public class ShopsDBContext : DbContext
    {
        public ShopsDBContext(DbContextOptions<ShopsDBContext> options) : base(options)
        {
                
        }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AdditionalAddress> AdditionalAddresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tickets> Tickets { get; set; }



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

            modelBuilder.Entity<User>()
                .Property(r => r.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();


        }
      
    }
}
