using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMSv1.Models
{
    public class Transaction_Product
    {
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }
        public int SalePrice { get; set; }
        
        // Exclusive for Addition Transaction
        public int PurchasePrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        [NotMapped] public int TotalPrice => Count * SalePrice;
    }
}