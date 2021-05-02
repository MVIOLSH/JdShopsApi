﻿// <auto-generated />
using JdShops.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JdShops.Migrations
{
    [DbContext(typeof(ShopsDBContext))]
    [Migration("20210502125658_init2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JdShops.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeliveryInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapCoordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ShopNumber")
                        .HasMaxLength(8)
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("JdShops.Entities.Shops", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Facia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ShopNumber")
                        .HasMaxLength(8)
                        .HasColumnType("real");

                    b.Property<string>("Town")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("JdShops.Entities.Shops", b =>
                {
                    b.HasOne("JdShops.Entities.Address", "Address")
                        .WithOne("Shop")
                        .HasForeignKey("JdShops.Entities.Shops", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("JdShops.Entities.Address", b =>
                {
                    b.Navigation("Shop");
                });
#pragma warning restore 612, 618
        }
    }
}
