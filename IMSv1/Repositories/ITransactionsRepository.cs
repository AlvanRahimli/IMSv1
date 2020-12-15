using System.Collections.Generic;
using System.Threading.Tasks;
using IMSv1.Models;
using IMSv1.Models.Dtos;

namespace IMSv1.Repositories
{
    public interface ITransactionsRepository
    {
        Task<List<Transaction>> GetTransactions(int userId);
        Task<Transaction> GetTransaction(int userId, int transactionId);
        Task<bool> AddTransaction(NSTransactionDto newTransaction, int userId);
    }
}