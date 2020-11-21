using System;
using System.Collections.Generic;

namespace IMSv1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Packaging { get; set; }
        public int StockCount { get; set; }
        public int OwnerId { get; set; }

        public User Owner { get; set; }
        public List<Transaction_Product> TransactionProducts { get; set; }
    }
}