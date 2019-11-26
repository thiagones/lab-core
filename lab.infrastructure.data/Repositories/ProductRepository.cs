using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab.domain.Interfaces.Repositories;
using lab.domain.Models.Data;
using lab.infrastructure.data.Database.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace lab.infrastructure.data.Repositories
{
    public class ProductRepository : BaseRepository<ProductDataModel>, IProductRepository
    {
        private readonly IMongoConnection _context;
        
        public ProductRepository(IMongoConnection mongoContext)
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