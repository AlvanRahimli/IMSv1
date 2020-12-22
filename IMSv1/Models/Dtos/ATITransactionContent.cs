using System;

namespace IMSv1.Models.Dtos
{
    public class ATITransactionContent : TransactionContentDto
    {
        public string Name { get; set; }
        public decimal ProductionPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Packaging { get; set; }
    }
}