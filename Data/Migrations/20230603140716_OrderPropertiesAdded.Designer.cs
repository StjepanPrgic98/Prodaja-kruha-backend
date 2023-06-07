﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prodaja_kruha_backend.Data;

#nullable disable

namespace Prodaja_kruha_backend.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230603140716_OrderPropertiesAdded")]
    partial class OrderPropertiesAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.CustomerProperties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Property")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerProperties");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CustomersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CustomersId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.OrderProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Order_ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Property")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Order_ItemId");

                    b.ToTable("OrderProperties");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.Order_item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Completed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OrdersId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TargetDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("TargetDay")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrdersId");

                    b.ToTable("Order_Items");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhotoURL")
                        .HasColumnType("TEXT");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.ProductInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Order_itemId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Order_itemId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInfo");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.CustomerProperties", b =>
                {
                    b.HasOne("Prodaja_kruha_backend.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.Order", b =>
                {
                    b.HasOne("Prodaja_kruha_backend.Entities.Customer", "Customers")
                        .WithMany()
                        .HasForeignKey("CustomersId");

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.OrderProperty", b =>
                {
                    b.HasOne("Prodaja_kruha_backend.Entities.Order_item", "Order_Item")
                        .WithMany()
                        .HasForeignKey("Order_ItemId");

                    b.Navigation("Order_Item");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.Order_item", b =>
                {
                    b.HasOne("Prodaja_kruha_backend.Entities.Order", "Orders")
                        .WithMany()
                        .HasForeignKey("OrdersId");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.ProductInfo", b =>
                {
                    b.HasOne("Prodaja_kruha_backend.Entities.Order_item", null)
                        .WithMany("ProductsInfo")
                        .HasForeignKey("Order_itemId");

                    b.HasOne("Prodaja_kruha_backend.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Prodaja_kruha_backend.Entities.Order_item", b =>
                {
                    b.Navigation("ProductsInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
