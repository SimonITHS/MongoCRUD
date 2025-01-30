using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBDemoo.models
{
    public class Courses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }
}
