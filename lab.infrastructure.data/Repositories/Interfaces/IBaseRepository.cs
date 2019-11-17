using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab.infrastructure.data.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);

         Task<IList<TEntity>> GetAllAsync();

         Task<TEntity> GetByIdAsync(object id);
    }
}