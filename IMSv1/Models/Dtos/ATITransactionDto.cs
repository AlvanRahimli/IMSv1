using System;
using System.Collections.Generic;

namespace IMSv1.Models.Dtos
{
    public class ATITransactionDto
    {
        public string Description { get; set; }
        public ATITransactionContent Content { get; set; }
    }
}