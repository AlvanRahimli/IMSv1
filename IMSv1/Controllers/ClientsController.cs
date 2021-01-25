using System.Threading.Tasks;
using IMSv1.Extensions;
using IMSv1.Models;
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

        [HttpGet]
        public async Task<IActionResult> AddClient()
        {
            var users = await _repo.GetUsers(HttpContext.GetUserId());
            ViewData["users"] = users;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(AddClientDto input)
        {
            var result = await _repo.AddClient(input, HttpContext.GetUserId());
            return RedirectToAction(result ? "Success" : "Error", "Home");
        }
    }
}