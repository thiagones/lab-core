using System.Collections.Generic;
using System.Threading.Tasks;
using lab.domain.Models;

namespace lab.domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductModel> AddAsync(ProductModel product);
        Task<IList<ProductModel>> GetByNameAsync(string productName);
        Task<IList<ProductModel>> GetByCodeAsync(string productCode);
        Task<ProductModel> GetByIdAsync(string productId);
    }
}