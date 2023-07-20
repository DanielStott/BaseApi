using System;
using MongoDB.Driver;

namespace Data;

public class MongoDatabaseFactory
{
    private readonly string? _connectionString;
    private readonly string _databaseName;

    public MongoDatabaseFactory(string? connectionString, string databaseName)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), "MongoDB connection string cannot be null.");
        _databaseName = databaseName;
    }

    public IMongoDatabase CreateDatabase()
    {
        var mongoClient = new MongoClient(_connectionString);
        return mongoClient.GetDatabase(_databaseName);
    }
}