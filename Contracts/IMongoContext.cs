using MongoDB.Driver;

namespace Contracts;
public interface IMongoContext
{
    IMongoDatabase Database { get; }
    IMongoCollection<T> GetCollection<T>() where T : class;
}
