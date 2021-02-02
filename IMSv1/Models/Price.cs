using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMSv1.Models
{
    public class Price
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }
        public DateTime AdditionDate { get; set; }
    }
}