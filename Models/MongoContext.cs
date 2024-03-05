using MongoDB.Driver;
using System;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext()
    {
        DotNetEnv.Env.Load();

        var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
        {
            throw new ApplicationException("MongoDB connection string or database name is missing in the environment variables.");
        }

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    public void DBInsertOne<T>(string collectionName, T obj)
    {
        var collection = _database.GetCollection<T>(collectionName);
        collection.InsertOne(obj);
    }

    public T DBSearchOne<T>(string collectionName, Expression<Func<T, bool>> filter)
    {
        var collection = _database.GetCollection<T>(collectionName);
        return collection.Find(filter).FirstOrDefault();
    }

    public IEnumerable<T> DBSearchAll<T>(string collectionName)
    {
        var collection = _database.GetCollection<T>(collectionName);
        return collection.Find(Builders<T>.Filter.Empty).ToList();
    }

    public void DBUpdateOne<T>(string collectionName, Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
    {
        var collection = _database.GetCollection<T>(collectionName);
        collection.UpdateOne(filter, update);
    }

}
