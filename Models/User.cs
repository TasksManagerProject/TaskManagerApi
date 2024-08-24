using MongoDB.Bson.Serialization.Attributes;

namespace TasksManagerApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("LastName")]
        public string LastName { get; set; } = null!;

        [BsonElement("Address")]
        public string Address { get; set; } = null!;

        [BsonElement("Email")]
        public string Email { get; set; } = null!;

        [BsonElement("Username")]
        public string Username { get; set; } = null!;

        [BsonElement("Phone")]
        public string Phone { get; set; } = null!;

        [BsonElement("Status")]
        public int Status { get; set; }
    }
}
