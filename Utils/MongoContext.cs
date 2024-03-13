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

}
