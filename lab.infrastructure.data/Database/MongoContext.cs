using System;
using lab.infrastructure.data.Database.Interfaces;
using lab.infrastructure.data.Settings.Interfaces;
using MongoDB.Driver;

namespace lab.infrastructure.data.Database
{
    public class MongoContext: IMongoContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(databaseSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>() where T : class
        {
            if (_database == null)
            {
                throw new ApplicationException("Banco de dados não conectado");
            }

            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}