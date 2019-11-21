using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab.infrastructure.data.Database.Interfaces;
using lab.infrastructure.data.Models;
using lab.infrastructure.data.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace lab.infrastructure.data.Repositories
{
    public class ProductRepository : BaseRepository<ProductDataModel>, IProductRepository
    {
        private readonly IMongoContext _context;
        
        public ProductRepository(IMongoContext mongoContext)
        : base(mongoContext)
        {
            _context = mongoContext;
        }

        public async Task<IList<ProductDataModel>> GetByCodeAsync(string productCode)
        {
            IList<ProductDataModel> products =
                 await _context.GetCollection<ProductDataModel>()
                     .AsQueryable()
                     .Where(x => x.Code.Contains(productCode))
                     .ToListAsync();

            return products;
        }
        public async Task<IList<ProductDataModel>> GetByNameAsync(string productName)
        {
            IList<ProductDataModel> products =
                await _context.GetCollection<ProductDataModel>()
                    .AsQueryable()
                    .Where(x => x.Name.Contains(productName))
                    .ToListAsync();

            return products;
        }
    }
}