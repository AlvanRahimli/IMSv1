using System;

namespace IMSv1.Models
{
    public class Price
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Value { get; set; }
        public DateTime AdditionDate { get; set; }
    }
}