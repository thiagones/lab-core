using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace lab.infrastructure.data.Models
{
    public class ProductDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Flavor { get; set; }
    }
}