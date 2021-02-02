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
        [Column(TypeName = "decimal(10,2)")]
        public decimal SalePrice { get; set; }
        public int OwnerId { get; set; }

        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public User Owner { get; set; }
        public List<Transaction_Product> TransactionProducts { get; set; }

        [NotMapped] 
        public decimal SalePriceFormatted => SalePrice;

        [NotMapped]
        public List<decimal> ProdPriceFormatted => ProductionPrices?.Select(pp => pp.Value).ToList();
        
        // TODO:
        [NotMapped] public int TotalProductionPrice => StockCount;

        [NotMapped] public decimal TotalSalePrice => StockCount * SalePrice;
    }
}