using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMSv1.Models.Dtos
{
    public class NSTransactionDto
    {
        [Required]
        public string Description { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public List<TransactionContentDto> Content { get; set; }
    }
}