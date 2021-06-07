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
        public  DbSet<ImgShop> ImgShop { get; set; }
        public DbSet<ImgAdditionalAddress> ImgAdditionalAddresses { get; set; }
        public DbSet<ImgTickets> ImgTickets { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Announcements> Announcements { get; set; }




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
            modelBuilder.Entity<Announcements>()
                .Property(r => r.Id)
                .IsRequired();
            modelBuilder.Entity<ImgShop>()
                .Property(r => r.Id)
                .IsRequired();
            modelBuilder.Entity<ImgAdditionalAddress>()
                .Property(r => r.Id)
                .IsRequired();
            modelBuilder.Entity<ImgTickets>()
                .Property(r => r.Id)
                .IsRequired();
            modelBuilder.Entity<File>()
                .Property(r => r.Id)
                .IsRequired();

        }
      
    }
}
