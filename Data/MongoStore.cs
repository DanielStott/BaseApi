using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Data;

public class MongoStore<T> : IMongoStore<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoStore(IMongoDatabase database, string collectionName) =>
        _collection = database.GetCollection<T>(collectionName);

    public async Task<T?> Find(Expression<Func<T, bool>> predicate) =>
        await _collection.AsQueryable().FirstOrDefaultAsync(predicate);

    public async Task<IAsyncCursor<T?>> FindAll(Expression<Func<T, bool>> predicate) =>
        await _collection.FindAsync<T>(predicate);

    public async Task Insert(T document) =>
        await _collection.InsertOneAsync(document);

    public async Task Replace(Expression<Func<T, bool>> predicate, T document) =>
        await _collection.ReplaceOneAsync(predicate, document);

    public async Task Delete(Expression<Func<T, bool>> predicate) =>
        await _collection.DeleteOneAsync(predicate);
}

public interface IMongoStore<T>
{
    Task<T?> Find(Expression<Func<T, bool>> predicate);
    Task<IAsyncCursor<T?>> FindAll(Expression<Func<T, bool>> predicate);
    Task Insert(T document);
    Task Replace(Expression<Func<T, bool>> predicate, T document);
    Task Delete(Expression<Func<T, bool>> predicate);
}