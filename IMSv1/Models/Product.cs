using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IMSv1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Packaging { get; set; }
        public int StockCount { get; set; }
        public List<Price> ProductionPrices { get; set; }
        public int SalePrice { get; set; }
        public int OwnerId { get; set; }

        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public User Owner { get; set; }
        public List<Transaction_Product> TransactionProducts { get; set; }

        [NotMapped] 
        public decimal SalePriceFormatted => (decimal) SalePrice / 100;

        [NotMapped]
        public List<decimal> ProdPriceFormatted => ProductionPrices?.Select(pp => (decimal) pp.Value / 100).ToList();
        
        // TODO:
        [NotMapped] public int TotalProductionPrice => StockCount;

        [NotMapped] public decimal TotalSalePrice => (decimal)StockCount * SalePrice / 100;
    }
}