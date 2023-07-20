using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Shared.Interfaces;
using Domain.Shared.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Data;

public class MongoStore<T> : IMongoStore<T> where T : Entity
{
    private readonly IMongoCollection<T> _collection;

    public MongoStore(IMongoDatabase database) =>
        _collection = database.GetCollection<T>($"{typeof(T).Name}s");

    public async Task<T?> Find(Expression<Func<T, bool>> predicate) =>
        await _collection.AsQueryable().FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate) =>
        (await _collection.AsQueryable().Where(predicate).ToCursorAsync()).ToEnumerable();

    public async Task Insert(T document) =>
        await _collection.InsertOneAsync(document);

    public async Task InsertMany(IEnumerable<T> documents) =>
        await _collection.InsertManyAsync(documents);

    public async Task Replace(Expression<Func<T, bool>> predicate, T document) =>
        await _collection.ReplaceOneAsync(predicate, document);

    public async Task Delete(Expression<Func<T, bool>> predicate) =>
        await _collection.DeleteOneAsync(predicate);
}