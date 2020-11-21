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
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.From)
                .WithMany(u => u.IssuedTransactions)
                .HasForeignKey(t => t.FromId);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.To)
                .WithMany(u => u.AcceptedTransactions)
                .HasForeignKey(t => t.ToId);

            modelBuilder.Entity<Transaction_Product>()
                .HasKey(tp => new {tp.ProductId, tp.TransactionId});
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transaction_Product> TransactionProducts { get; set; }
    }
}