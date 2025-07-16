using Contracts;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Repository;
public class MongoContext : IMongoContext
{
    public IMongoDatabase Database { get; }

    public MongoContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var client = new MongoClient(connectionString);
        var databaseName = MongoUrl.Create(connectionString).DatabaseName;
        Database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>() where T : class
    {
        return Database.GetCollection<T>(typeof(T).Name);
    }
}
