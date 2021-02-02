// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMSv1.Models
{
    public class UserClient
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int? ClientId { get; set; }
        public User Client { get; set; }
        
        public string ClientName { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal Debt { get; set; }
        public DateTime LastSaleDate { get; set; }
    }
}