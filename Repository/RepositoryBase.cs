using Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Repository;
public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;
    protected readonly IMongoContext _context;

    // Constructor that accepts an IMongoContext to get the database collection
    public RepositoryBase(IMongoContext context)
    {
        _context = context;
        _collection = _context.GetCollection<T>();
    }

    public async Task<IEnumerable<T>> FindAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression) =>
        await _collection.Find(expression).ToListAsync();

    public async Task<T> FindByIdAsync(string id)
    {
        var objectId = ObjectId.Parse(id);        

        var filter = Builders<T>.Filter.Eq("_id", objectId);

        return await _collection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        // Assumes your entity has an Id property
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} must have an Id property");

        var id = idProperty.GetValue(entity);

        var filter = Builders<T>.Filter.Eq("_id", id);

        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var objectId = ObjectId.Parse(id);

        var filter = Builders<T>.Filter.Eq("_id", objectId);

        await _collection.DeleteOneAsync(filter);
    }

    public async Task DeleteAsync(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} must have an Id property");

        var id = idProperty.GetValue(entity);

        var filter = Builders<T>.Filter.Eq("_id", id);

        await _collection.DeleteOneAsync(filter);
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var objectId = ObjectId.Parse(id);

        var filter = Builders<T>.Filter.Eq("_id", objectId);

        var count = await _collection.CountDocumentsAsync(filter);

        return count > 0;
    }

    public async Task<long> CountAsync()
    {
        return await _collection.CountDocumentsAsync(_ => true);
    }

    public async Task<long> CountAsync(Expression<Func<T, bool>> expression)
    {
        return await _collection.CountDocumentsAsync(expression);
    }
}
