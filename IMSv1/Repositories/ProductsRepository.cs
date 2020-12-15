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
    public class ProductsRepository  : IProductsRepository
    {
        private readonly AnbarContext _context;

        public ProductsRepository(AnbarContext context)
        {
            _context = context;
        }
        
        public async Task<List<Product>> GetAllProducts(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return null;

            var products = await _context.Products
                .AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.ProductionPrices)
                .Where(p => p.OwnerId == userId)
                .ToListAsync();

            return products;
        }

        public async Task<Product> GetProduct(int id, int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            var product = await _context.Products
                .Include(p => p.Owner)
                .Include(p => p.ProductionPrices)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (user.Role == Role.Manager)
            {
                return product;
            }

            return product.OwnerId == userId ? product : null;
        }

        public async Task<bool> AddProduct(ATITransactionDto tATITransactionDto, int userId, bool isNew)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;

            var transaction = new Transaction
            {
                Date = DateTime.Now,
                Description = tATITransactionDto.Description,
                FromId = userId,
                ToId = userId,
                Type = TransactionType.Addition,
                TotalAmount = tATITransactionDto.Content.Count * tATITransactionDto.Content.ProductionPrice
            };

            if (isNew)
            {
                var product = new Product
                {
                    Name = tATITransactionDto.Content.Name,
                    Packaging = tATITransactionDto.Content.Packaging,
                    ExpirationDate = tATITransactionDto.Content.ExpirationDate,
                    ProductionDate = tATITransactionDto.Content.ProductionDate,
                    SalePrice = tATITransactionDto.Content.SalePrice,
                    StockCount = tATITransactionDto.Content.Count,
                    OwnerId = userId,
                    ProductionPrices = new[] {new Price
                    {
                        Value = tATITransactionDto.Content.ProductionPrice,
                        AdditionDate = DateTime.Now
                    } }.ToList()
                };
                var transactionProduct = new Transaction_Product
                {
                    Transaction = transaction,
                    Product = product,
                    Count = tATITransactionDto.Content.Count,
                    SalePrice = tATITransactionDto.Content.SalePrice
                };

                await _context.TransactionProducts.AddAsync(transactionProduct);
                Console.WriteLine("ADDED AS NEW PRODUCT");
            }
            else
            {
                var transactionProduct = new Transaction_Product
                {
                    Transaction = transaction,
                    ProductId = tATITransactionDto.Content.ProductId,
                    Count = tATITransactionDto.Content.Count,
                    SalePrice = tATITransactionDto.Content.SalePrice
                };

                var product = await _context.Products
                    .Include(p => p.ProductionPrices)
                    .FirstOrDefaultAsync(p => p.Id == tATITransactionDto.Content.ProductId);
                product.StockCount += tATITransactionDto.Content.Count;
                product.ProductionPrices.Add(new Price()
                {
                    ProductId = product.Id,
                    Value = tATITransactionDto.Content.ProductionPrice,
                    AdditionDate = DateTime.Now
                });

                await _context.TransactionProducts.AddAsync(transactionProduct);
                Console.WriteLine("ADDED AS EXISTING PRODUCT");
            }
            
            var dbRes = await _context.SaveChangesAsync();
            return dbRes > 0;
        }

        public async Task<bool> DeleteProduct(int id, int userId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return false;

            if (product.OwnerId == userId || userId == 1)
            {
                _context.Products.Remove(product);
            }

            var dbRes = await _context.SaveChangesAsync();
            return dbRes > 0;
        }
    }
}