using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMSv1.Data;
using IMSv1.Models;
using IMSv1.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IMSv1.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AnbarContext _context;

        public UsersRepository(AnbarContext context)
        {
            _context = context;
        }
        
        public async Task<List<ClientDto>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new ClientDto
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToListAsync();
            return users;
        }

        public async Task<int> AddUser(User input)
        {
            await _context.Users.AddAsync(input);
            var dbRes = await _context.SaveChangesAsync();
            return dbRes > 0 ? input.Id : 0;
        }
        
        public async Task<List<ClientDto>> GetUsers(int id)
        {
            var users = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id != id)
                .Select(u => new ClientDto
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToListAsync();
            return users;
        }

        public async Task<List<UserClient>> GetClientsForUser(int userId)
        {
            var clients = await _context.UserClients
                .AsNoTracking()
                .Include(c => c.Client)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.LastSaleDate)
                .ToListAsync();
            
            return clients;
        }
        
        public async Task<UserClient> GetClient(int userId, int clientId)
        {
            var client = await _context.UserClients
                .AsNoTracking()
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ClientId == clientId);

            return client;
        }

        public async Task<bool> AddPayment(int userId, int clientId, int amount)
        {
            var client = await _context.UserClients
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ClientId == clientId);
            if (client == null)
                return false;
            
            client.Debt -= amount;
            var dbRes = await _context.SaveChangesAsync();
            return dbRes > 0;
        }
    }
}