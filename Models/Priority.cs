using MongoDB.Bson.Serialization.Attributes;

namespace TasksManagerApi.Models
{
    public class Priority
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("Name")]
        public string Name { get; set; } = null!;
    }
}
