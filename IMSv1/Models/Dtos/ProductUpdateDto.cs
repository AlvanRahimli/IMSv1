using System;
using System.Collections.Generic;

namespace IMSv1.Models.Dtos
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Packaging { get; set; }
        public int StockCount { get; set; }
        public List<Price> ProductionPrices { get; set; }
        public decimal SalePrice { get; set; }
        public decimal OwnerId { get; set; }

        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}