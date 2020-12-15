using System.Threading.Tasks;
using IMSv1.Extensions;
using IMSv1.Models.Dtos;
using IMSv1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMSv1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _repo;
        public ProductsController(IProductsRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.GetUserId();
            var products = await _repo.GetAllProducts(userId);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> PostProduct()
        {
            var userId = HttpContext.GetUserId();
            var products = await _repo.GetAllProducts(userId);
            
            ViewData["products"] = products;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> PostProduct(ATITransactionDto @new)
        {
            var userId = HttpContext.GetUserId();
            var isSuccess = await _repo.AddProduct(@new, userId, @new.Content.ProductId == -1);
            if (isSuccess)
            {
                return RedirectToAction("Index", "Products");
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = HttpContext.GetUserId();
            var product = await _repo.GetProduct(id, userId);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetDelete(int id)
        {
            var userId = HttpContext.GetUserId();
            var product = await _repo.GetProduct(id, userId);
            return View("Delete", product);
        }

        [HttpPost]
        public async Task<IActionResult> PostDelete(int id)
        {
            var userId = HttpContext.GetUserId();
            var isSuccess = await _repo.DeleteProduct(id, userId);
            if (isSuccess)
                return RedirectToAction("Success", "Home");

            return RedirectToAction("Error", "Home");
        }
    }
}