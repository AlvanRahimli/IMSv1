﻿// <auto-generated />
using System;
using IMSv1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IMSv1.Migrations
{
    [DbContext(typeof(AnbarContext))]
    [Migration("20210125220717_SwitchToDecimal")]
    partial class SwitchToDecimal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IMSv1.Models.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AdditionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Prices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdditionDate = new DateTime(2021, 1, 26, 2, 7, 17, 475, DateTimeKind.Local).AddTicks(2023),
                            ProductId = 1,
                            Value = 150m
                        });
                });

            modelBuilder.Entity("IMSv1.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Packaging")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("StockCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExpirationDate = new DateTime(2021, 1, 31, 2, 7, 17, 471, DateTimeKind.Local).AddTicks(619),
                            Name = "prod 1",
                            OwnerId = 1,
                            Packaging = "150 qram",
                            ProductionDate = new DateTime(2021, 1, 21, 2, 7, 17, 474, DateTimeKind.Local).AddTicks(9162),
                            SalePrice = 120m,
                            StockCount = 100
                        });
                });

            modelBuilder.Entity("IMSv1.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FromId")
                        .HasColumnType("int");

                    b.Property<int>("ToId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("IMSv1.Models.Transaction_Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ProductId", "TransactionId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionProducts");
                });

            modelBuilder.Entity("IMSv1.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contact")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("District")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Contact = "alvanrahimli@gmail.com",
                            District = "Lerik",
                            Name = "alvan",
                            Password = "alvan12345",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            Contact = "051 510 12 43",
                            District = "Baki",
                            Name = "ali",
                            Password = "ali12345",
                            Role = 1
                        });
                });

            modelBuilder.Entity("IMSv1.Models.UserClient", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Debt")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("LastSaleDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId", "ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("UserClients");
                });

            modelBuilder.Entity("IMSv1.Models.Price", b =>
                {
                    b.HasOne("IMSv1.Models.Product", "Product")
                        .WithMany("ProductionPrices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMSv1.Models.Product", b =>
                {
                    b.HasOne("IMSv1.Models.User", "Owner")
                        .WithMany("Products")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMSv1.Models.Transaction", b =>
                {
                    b.HasOne("IMSv1.Models.User", "From")
                        .WithMany("IssuedTransactions")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMSv1.Models.User", "To")
                        .WithMany("AcceptedTransactions")
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMSv1.Models.Transaction_Product", b =>
                {
                    b.HasOne("IMSv1.Models.Product", "Product")
                        .WithMany("TransactionProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMSv1.Models.Transaction", "Transaction")
                        .WithMany("Content")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMSv1.Models.UserClient", b =>
                {
                    b.HasOne("IMSv1.Models.User", "Client")
                        .WithMany("IsClientOf")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMSv1.Models.User", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}