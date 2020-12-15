using System;
using System.Collections.Generic;

namespace IMSv1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Contact { get; set; }
        public string District { get; set; }

        public List<Transaction> IssuedTransactions { get; set; }
        public List<Transaction> AcceptedTransactions { get; set; }
        
        public List<Product> Products { get; set; }
        public List<UserClient> Clients { get; set; }
        public List<UserClient> IsClientOf { get; set; }
    }
}