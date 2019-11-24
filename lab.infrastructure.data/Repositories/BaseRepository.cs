using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lab.domain.Interfaces.Repositories;
using lab.infrastructure.data.Database.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace lab.infrastructure.data.Repositories
{
    public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IMongoContext _context;
        public BaseRepository(IMongoContext mongoContext)
        {
            _context = mongoContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.GetCollection<TEntity>().InsertOneAsync(entity);

            return entity;
        }
        
        public async Task<IList<TEntity>> GetAllAsync()
        {
            IList<TEntity> entities =
                await _context.GetCollection<TEntity>()
                    .AsQueryable()
                    .ToListAsync();

            return entities;
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            
            var filter = new BsonDocument("_id", id.ToString());

            var document = await _context.GetCollection<TEntity>()
                    .FindAsync(filter);

            var jsonDocument = document.ToJson();
            
            TEntity result = JsonConvert.DeserializeObject<TEntity>(jsonDocument);
            
            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}