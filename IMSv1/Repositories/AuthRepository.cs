using System.Threading.Tasks;
using IMSv1.Data;
using IMSv1.Models;
using IMSv1.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IMSv1.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AnbarContext _context;

        public AuthRepository(AnbarContext context)
        {
            _context = context;
        }
        
        public async Task<User> Login(UserLoginDto loginCreds)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Name == loginCreds.Name && u.Password == loginCreds.Password);
            return user;
        }
    }
}