using System.Collections.Generic;

namespace IMSv1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public int Debt { get; set; }

        public List<Transaction> IssuedTransactions { get; set; }
        public List<Transaction> AcceptedTransactions { get; set; }
        public List<Product> Products { get; set; }
    }
}