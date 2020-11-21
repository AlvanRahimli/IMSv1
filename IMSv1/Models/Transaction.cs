using System;
using System.Collections.Generic;

namespace IMSv1.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int TotalAmount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }

        public List<Transaction_Product> Content { get; set; }
        
        public int FromId { get; set; }
        public User From { get; set; }

        public int ToId { get; set; }
        public User To { get; set; }
    }
}