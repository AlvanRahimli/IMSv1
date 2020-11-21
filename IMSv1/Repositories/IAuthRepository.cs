using System.Threading.Tasks;
using IMSv1.Models;
using IMSv1.Models.Dtos;

namespace IMSv1.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Login(UserLoginDto loginCreds);
    }
}