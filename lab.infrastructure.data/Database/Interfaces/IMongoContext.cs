using MongoDB.Driver;

namespace lab.infrastructure.data.Database.Interfaces
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>() where T : class;
    }
}