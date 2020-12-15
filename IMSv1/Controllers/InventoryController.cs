using System.Threading.Tasks;
using IMSv1.Extensions;
using IMSv1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IMSv1.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IProductsRepository _repo;

        public InventoryController(IProductsRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.GetUserId();
            var inventory = await _repo.GetAllProducts(userId);
            return View("Inventory", inventory);
        }
    }
}
