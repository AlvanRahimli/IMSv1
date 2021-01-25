using System;
using System.Collections.Generic;
using IMSv1.Models;
using Microsoft.EntityFrameworkCore;

namespace IMSv1.Data
{
    public class AnbarContext : DbContext
    {
        public AnbarContext(DbContextOptions<AnbarContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                    {
                        Id = 1,
                        Name = "alvan",
                        Password = "alvan12345",
                        Role = Role.Manager,
                        District = "Lerik",
                        Contact = "alvanrahimli@gmail.com"
                    },
                    new User
                    {
                        Id = 2,
                        Name = "ali",
                        Password = "ali12345",
                        Role = Role.User,
                        Contact = "051 510 12 43",
                        District = "Baki"
                    });

            modelBuilder.Entity<Product>()
                .HasData(new Product
                {
                    Id = 1,
                    Name = "prod 1",
                    OwnerId = 1,
                    Packaging = "150 qram",
                    ExpirationDate = DateTime.Now.AddDays(5),
                    ProductionDate = DateTime.Now.AddDays(-5),
                    SalePrice = 120,
                    StockCount = 100
                });
            modelBuilder.Entity<Price>()
                .HasData(new Price()
                {
                    Id = 1,
                    Value = 150,
                    AdditionDate = DateTime.Now,
                    ProductId = 1
                });
            
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.From)
                .WithMany(u => u.IssuedTransactions)
                .HasForeignKey(t => t.FromId);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.To)
                .WithMany(u => u.AcceptedTransactions)
                .HasForeignKey(t => t.ToId);

            modelBuilder.Entity<UserClient>()
                .HasKey(uc => new {uc.UserId, uc.ClientId});
            modelBuilder.Entity<UserClient>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.Clients)
                .HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserClient>()
                .HasOne(uc => uc.Client)
                .WithMany(u => u.IsClientOf)
                .HasForeignKey(uc => uc.ClientId);

            modelBuilder.Entity<Transaction_Product>()
                .HasKey(tp => new {tp.ProductId, tp.TransactionId});
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transaction_Product> TransactionProducts { get; set; }
        public DbSet<UserClient> UserClients { get; set; }
        public DbSet<Price> Prices { get; set; }
    }
}