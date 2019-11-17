using System.Collections.Generic;
using System.Threading.Tasks;
using lab.infrastructure.data.Models;

namespace lab.infrastructure.data.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<ProductDataModel>
    {
        Task<IList<ProductDataModel>> GetByCodeAsync(string productCode);   
        Task<IList<ProductDataModel>> GetByNameAsync(string productName);   
    }
}