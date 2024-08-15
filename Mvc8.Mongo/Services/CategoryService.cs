using MongoDB.Bson;
using MongoDB.Driver;
using Mvc8.Mongo.Models.MongoDb;
using Mvc8.Mongo.Repository;

namespace Mvc8.Mongo.Services;

public class CategoryService
{
    private readonly IMongoRepository<Category> _categoryRepository;

    public CategoryService(IMongoRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Category> GetByIdAsync(string id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }
    
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }
    
    public async Task<List<Category>> GetThirdLevelCategoriesAsync()
    {
        var pipeline = new[]
        {
            new BsonDocument("$match", new BsonDocument
            {
                { "ancestor", new BsonDocument("$ne", BsonNull.Value) },
                { "parent", new BsonDocument("$ne", BsonNull.Value) }
            }),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "categories" },
                { "localField", "parent" },
                { "foreignField", "_id" },
                { "as", "parentCategory" }
            }),
            new BsonDocument("$unwind", "$parentCategory"),
            new BsonDocument("$match", new BsonDocument
            {
                { "parentCategory.parent", new BsonDocument("$ne", BsonNull.Value) }
            }),
            new BsonDocument("$project", new BsonDocument
            {
                { "_id", 1 },
                { "name", 1 },
                { "ancestor", 1 },
                { "parent", 1 }
            })
        };

        var result = await _categoryRepository.Collection.Aggregate<Category>(pipeline).ToListAsync();
        return result;
    }
    
    public async Task<string> GetCategoryIdByNameAsync(string name)
    {
        var filter = Builders<Category>.Filter.Eq("name", name);
        var category = await _categoryRepository.Collection.Find(filter).FirstOrDefaultAsync();
        return category.Id;
    }
}