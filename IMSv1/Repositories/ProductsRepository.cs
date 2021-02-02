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
            foreach (var product in products)
            {
                product.ProductionPrices = product.ProductionPrices.OrderByDescending(p => p.AdditionDate).ToList();
            }

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

        public async Task<bool> AddProduct(ATITransactionDto tAtiTransactionDto, int userId, bool isNew)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;

            var price = tAtiTransactionDto.Content.ProductionPrice;
            var salePrice = tAtiTransactionDto.Content.SalePrice;

            var transaction = new Transaction
            {
                Date = DateTime.Now,
                Description = tAtiTransactionDto.Description,
                FromId = userId,
                ToId = userId,
                Type = TransactionType.Addition,
                TotalAmount = tAtiTransactionDto.Content.Count * price
            };

            if (isNew)
            {
                var product = new Product
                {
                    Name = tAtiTransactionDto.Content.Name,
                    Packaging = tAtiTransactionDto.Content.Packaging,
                    ExpirationDate = tAtiTransactionDto.Content.ExpirationDate,
                    ProductionDate = tAtiTransactionDto.Content.ProductionDate,
                    SalePrice = salePrice,
                    StockCount = tAtiTransactionDto.Content.Count,
                    OwnerId = userId,
                    ProductionPrices = new List<Price>
                    {
                        new Price
                        {
                            Value = price,
                            AdditionDate = DateTime.Now
                        }
                    }
                };
                var transactionProduct = new Transaction_Product
                {
                    Transaction = transaction,
                    Product = product,
                    Count = tAtiTransactionDto.Content.Count,
                    SalePrice = salePrice
                };

                await _context.TransactionProducts.AddAsync(transactionProduct);
                Console.WriteLine("ADDED AS NEW PRODUCT");
            }
            else
            {
                var transactionProduct = new Transaction_Product
                {
                    Transaction = transaction,
                    ProductId = tAtiTransactionDto.Content.ProductId,
                    Count = tAtiTransactionDto.Content.Count,
                    SalePrice = salePrice
                };

                var product = await _context.Products
                    .Include(p => p.ProductionPrices)
                    .FirstOrDefaultAsync(p => p.Id == tAtiTransactionDto.Content.ProductId);
                product.StockCount += tAtiTransactionDto.Content.Count;
                product.ProductionPrices.Add(new Price
                {
                    ProductId = product.Id,
                    Value = price,
                    AdditionDate = DateTime.Now
                });

                await _context.TransactionProducts.AddAsync(transactionProduct);
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

        public async Task<bool> UpdateProduct(int userId, ProductUpdateDto input)
        {
            var product = await _context.Products
                .Include(p => p.ProductionPrices)
                .FirstOrDefaultAsync(p => p.Id == input.Id && p.OwnerId == userId);
            if (product == null)
            {
                return false;
            }

            product.Name = input.Name;
            product.Packaging = input.Packaging;
            product.StockCount = input.StockCount;
            product.ExpirationDate = input.ExpirationDate;
            product.ProductionDate = input.ProductionDate;
            product.SalePrice = input.SalePrice;
            product.ProductionPrices
                .OrderByDescending(p => p.AdditionDate)
                .ToList()[0].Value = input.ProductionPrice;

            var dbRes = await _context.SaveChangesAsync();
            return dbRes > 0;
        }
    }
}