using MongoDB.Bson.Serialization.Attributes;

namespace TasksManagerApi.Models
{
    public class Task
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Id { get; set; } = null!;

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("IsCompleted")]
        public bool IsCompleted { get; set; }

        [BsonElement("Status")]
        public short Status { get; set; }

        [BsonElement("PriorityId")]
        public string PriorityId { get; set; } = null!;

        [BsonElement("UserId")]
        public string UserId { get; set; } = null!;
    }
}
