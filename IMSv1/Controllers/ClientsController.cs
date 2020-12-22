using System.Threading.Tasks;
using IMSv1.Extensions;
using IMSv1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMSv1.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IUsersRepository _repo;

        public ClientsController(IUsersRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.GetUserId();
            var clients = await _repo.GetClientsForUser(userId);
            
            return View(clients);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(int clientId, decimal amount)
        {
            amount *= 100;
            var intAmount = (int) amount;
            var userId = HttpContext.GetUserId();
            var res = await _repo.AddPayment(userId, clientId, intAmount);
            if (res)
            {
                return RedirectToAction("Success", "Home");
            }

            return RedirectToAction("Error", "Home");
        }
    }
}