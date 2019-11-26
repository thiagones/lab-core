using MongoDB.Driver;

namespace lab.infrastructure.data.Database.Interfaces
{
    public interface IMongoConnection
    {
        IMongoCollection<T> GetCollection<T>() where T : class;
    }
}