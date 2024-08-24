using MongoDB.Driver;
using TasksManagerApi.Models;

namespace TasksManagerApi.Persistence
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString); // genera la conexión con la base de datos
            _database = client.GetDatabase(settings.DatabaseName); // obtiene la base de datos
        }

        public IMongoCollection<Priority> Priorities => _database.GetCollection<Priority>("Priorities"); // obtiene la colección de prioridades
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users"); // obtiene la colección de usuarios
    }
}
