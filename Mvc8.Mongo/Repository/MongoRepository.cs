using MongoDB.Bson;
using MongoDB.Driver;

namespace Mvc8.Mongo.Repository;

public class MongoRepository<T> : IMongoRepository<T> where T : class
{
    private readonly IMongoDatabase _database;
    private readonly string _collectionName;

    public IMongoCollection<T> Collection => _database.GetCollection<T>(_collectionName);

    public MongoRepository(IMongoClient client, string databaseName, string collectionName)
    {
        _database = client.GetDatabase(databaseName);
        _collectionName = collectionName;
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await Collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(string id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        await Collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        await Collection.DeleteOneAsync(filter);
    }
}