using System.Collections.Generic;
using System.Threading.Tasks;
using lab.domain.Models.Data;

namespace lab.domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<ProductDataModel>
    {
        Task<IList<ProductDataModel>> GetByCodeAsync(string productCode);   
        Task<IList<ProductDataModel>> GetByNameAsync(string productName);   
    }
}