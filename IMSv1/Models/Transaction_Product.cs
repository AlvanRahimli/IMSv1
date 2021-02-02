// ReSharper disable InconsistentNaming
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
        [Column(TypeName = "decimal(10,2)")]
        public decimal SalePrice { get; set; }

        [NotMapped] public decimal TotalPrice => Count * SalePrice;
    }
}