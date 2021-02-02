using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMSv1.Data;
using IMSv1.Models;
using IMSv1.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IMSv1.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly AnbarContext _context;
        private readonly IUsersRepository _usersRepository;

        public TransactionsRepository(AnbarContext context, IUsersRepository usersRepository)
        {
            _context = context;
            _usersRepository = usersRepository;
        }
        
        public async Task<List<Transaction>> GetTransactions(int userId)
        {
            var transactions = await _context.Transactions
                .Include(t => t.From)
                .Include(t => t.To)
                .Where(t => t.FromId == userId || t.ToId == userId)
                .Where(t => t.Type == TransactionType.Sale)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
            return transactions;
        }

        public async Task<Transaction> GetTransaction(int userId, int tId)
        {
            var transaction = await _context.Transactions
                .Include(t => t.From)
                .Include(t => t.To)
                .Include(t => t.Content)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(t => t.Id == tId);
            if (transaction != null)
            {
                if (transaction.ToId != userId && transaction.FromId != userId)
                    return null;
            }
            return transaction;
        }

        public async Task<bool> AddTransaction(NSTransactionDto newTransaction, int userId)
        {
            if (newTransaction.ClientId == 0 && string.IsNullOrEmpty(newTransaction.ClientName))
                return false;
            var addedUserId = 0;
            newTransaction.ClientName = newTransaction.ClientName?.Trim();
            if (newTransaction.ClientId == 0) // Create new user
            {
                var newUser = new User
                {
                    Name = newTransaction.ClientName,
                    District = newTransaction.ClientDistrict,
                    Contact = newTransaction.ClientPhone,
                    Password = "12345",
                    Role = Role.User
                };
                addedUserId = await _usersRepository.AddUser(newUser);
            }

            var isClient = await _context.UserClients
                .AsNoTracking()
                .AnyAsync(c => c.UserId == userId && c.ClientId == newTransaction.ClientId);
            if (!isClient)
            {
                UserClient newClient;
                if (newTransaction.ClientId == 0) // If we are adding user by name
                {
                    newClient = new UserClient
                    {
                        UserId = userId,
                        ClientId = addedUserId,
                        Debt = 0,
                        ClientName = newTransaction.ClientName
                    };
                }
                else
                {
                    newClient = new UserClient
                    {
                        UserId = userId,
                        ClientId = newTransaction.ClientId,
                        ClientName = "EXISTING_USER",
                        Debt = 0
                    };
                }
                await _context.UserClients.AddAsync(newClient);
                await _context.SaveChangesAsync();
            }

            var transaction = new Transaction
            {
                Description = newTransaction.Description,
                Date = DateTime.Now,
                FromId = userId,
                ToId = newTransaction.ClientId == 0 ? addedUserId : newTransaction.ClientId,
                TotalAmount = newTransaction.Content.Sum(dto => dto.SalePrice * dto.Count),
                Content = newTransaction.Content
                    .Where(dto => dto != null)
                    .Select(dto => new Transaction_Product
                    {
                        ProductId = dto.ProductId,
                        Count = dto.Count,
                        SalePrice = dto.SalePrice
                    }).ToList()
            };

            var clientUser = await _context.Users
                                 .Include(u => u.Products)
                                 .FirstOrDefaultAsync(u => u.Id == newTransaction.ClientId) ??
                             await _context.Users
                                 .Include(u => u.Products)
                                 .FirstOrDefaultAsync(u => u.Id == addedUserId);

            var client = await _context.UserClients // Find client by Id
                             .FirstOrDefaultAsync(c => c.ClientId == newTransaction.ClientId 
                                                       && c.UserId == userId) ?? 
                         await _context.UserClients // Find client by name 
                             .FirstOrDefaultAsync(c => c.ClientName == newTransaction.ClientName
                                                       && c.UserId == userId);
            if (client == null)
            {
                Console.WriteLine("CLIENT WAS NULL");
                return false;
            }

            if (newTransaction.Type == "return")
            {
                client.Debt -= transaction.TotalAmount;
                transaction.Type = TransactionType.Return;
            }
            else
            {
                client.Debt += transaction.TotalAmount;
                transaction.Type = TransactionType.Sale;
            }
            client.LastSaleDate = DateTime.Now;
            foreach (var p in transaction.Content)
            {
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(pr => pr.Id == p.ProductId);
                if (existingProduct == null)
                    continue;

                existingProduct.StockCount -= p.Count;
                // If client already owns this product
                var owns = clientUser.Products.Any(pr => pr.Name == existingProduct.Name);
                if (owns)
                {
                    var product = await _context.Products
                        .Include(pr => pr.ProductionPrices)
                        .FirstOrDefaultAsync(pr => pr.Name == existingProduct.Name && pr.OwnerId == clientUser.Id);
                    if (product == null)
                    {
                        Console.WriteLine("EXISTING_PRODUCT_NULL");
                        continue;
                    }

                    product.StockCount += p.Count;
                    product.ProductionPrices.Add(new Price
                    {
                        AdditionDate = DateTime.Now,
                        Value = p.SalePrice
                    });
                }
                else
                {
                    // Add product for client
                    var newProduct = new Product
                    {
                        Name = existingProduct.Name,
                        Packaging = existingProduct.Packaging,
                        ExpirationDate = existingProduct.ExpirationDate,
                        ProductionDate = existingProduct.ProductionDate,
                        ProductionPrices = new List<Price>
                        {
                            new Price {Value = p.SalePrice, AdditionDate = DateTime.Now}
                        },
                        SalePrice = p.SalePrice,
                        OwnerId = newTransaction.ClientId == 0 ? addedUserId : newTransaction.ClientId,
                        StockCount = p.Count
                    };
                    await _context.Products.AddAsync(newProduct);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.Transactions.AddAsync(transaction);
            var dbRes = await _context.SaveChangesAsync();
            return dbRes > 0;
        }
    }
}