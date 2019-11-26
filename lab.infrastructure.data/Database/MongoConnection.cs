using System;
using lab.domain.Models.Data;
using lab.infrastructure.data.Database.Interfaces;
using lab.infrastructure.data.Settings.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace lab.infrastructure.data.Database
{
    public class MongoConnection : IMongoConnection
    {
        private readonly IMongoDatabase _database = null;

        public MongoConnection(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            if (client != null)
            {
                BsonClassMap.RegisterClassMap<UserDataModel>(cm => 
                {
                    cm.AutoMap();
                    cm.MapIdProperty(c => c.Id)
                        .SetIdGenerator(StringObjectIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });

                BsonClassMap.RegisterClassMap<ProductDataModel>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(c => c.Id)
                        .SetIdGenerator(StringObjectIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });

                _database = client.GetDatabase(databaseSettings.DatabaseName);
            }
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