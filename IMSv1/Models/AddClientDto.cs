using System;

namespace IMSv1.Models
{
    public class AddClientDto
    {
        public string AdditionType { get; set; }
        
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public decimal Debt { get; set; }
        public string ClientDistrict { get; set; }
        public string ClientPhone { get; set; }
        public DateTime LastSaleDate { get; set; }
    }
}