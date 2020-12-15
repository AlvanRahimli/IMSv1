using System.Collections.Generic;
using System.Threading.Tasks;
using IMSv1.Models;
using IMSv1.Models.Dtos;

namespace IMSv1.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProducts(int userId);
        Task<Product> GetProduct(int id, int userId);
        Task<bool> AddProduct(ATITransactionDto tATITransactionDto, int userId, bool isNew);
        Task<bool> DeleteProduct(int id, int userId);
    }
}