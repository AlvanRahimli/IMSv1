using System.Collections.Generic;
using System.Threading.Tasks;
using IMSv1.Models;
using IMSv1.Models.Dtos;

namespace IMSv1.Repositories
{
    public interface IUsersRepository
    {
        Task<List<ClientDto>> GetUsers();
        Task<List<ClientDto>> GetUsers(int id);
        Task<int> AddUser(User input);
        Task<List<UserClient>> GetClientsForUser(int userId);
        Task<UserClient> GetClient(int userId, int clientId);
        Task<bool> AddPayment(int userId, int clientId, int amount);
    }
}