using System;
using System.Threading.Tasks;
using IMSv1.Extensions;
using IMSv1.Models.Dtos;
using IMSv1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMSv1.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepository _repo;
        private readonly IProductsRepository _productsRepository;
        private readonly IUsersRepository _usersRepository;

        public TransactionsController(ITransactionsRepository repo, 
            IProductsRepository productsRepository,
            IUsersRepository usersRepository)
        {
            _repo = repo;
            _productsRepository = productsRepository;
            _usersRepository = usersRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.GetUserId();
            var transactions = await _repo.GetTransactions(userId);
            return View(transactions);
        }

        public async Task<IActionResult> GetTransaction(int tId)
        {
            var userId = HttpContext.GetUserId();
            var transaction = await _repo.GetTransaction(userId, tId);
            if (transaction == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var userClient = await _usersRepository.GetClient(userId, transaction.ToId);
            ViewData["client"] = userClient;
            return View("TransactionDetails", transaction);
        }

        [HttpGet]
        public async Task<IActionResult> PostTransaction()
        {
            var userId = HttpContext.GetUserId();
            var products = await _productsRepository.GetAllProducts(userId);
            ViewData["products"] = products;
            var users = await _usersRepository.GetUsers(userId);
            ViewData["users"] = users;
            Console.WriteLine(users.Count);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostTransaction(NSTransactionDto newTransaction)
        {
            var userId = HttpContext.GetUserId();
            var isSuccessful = await _repo.AddTransaction(newTransaction, userId);

            if (isSuccessful)
            {
                return RedirectToAction("Success", "Home");
            }

            return RedirectToAction("Error", "Home");
        }
        
        [HttpGet]
        public async Task<IActionResult> PostReturn()
        {
            var userId = HttpContext.GetUserId();
            var products = await _productsRepository.GetAllProducts(userId);
            ViewData["products"] = products;
            var users = await _usersRepository.GetUsers(userId);
            ViewData["users"] = users;
            Console.WriteLine(users.Count);
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> PostReturn(NSTransactionDto newTransaction)
        {
            var userId = HttpContext.GetUserId();
            var isSuccessful = await _repo.AddTransaction(newTransaction, userId);

            if (isSuccessful)
            {
                return RedirectToAction("Success", "Home");
            }

            return RedirectToAction("Error", "Home");
        }
    }
}