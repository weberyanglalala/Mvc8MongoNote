using MongoDB.Bson;
using MongoDB.Driver;
using Mvc8.Mongo.Models.MongoDb;
using Mvc8.Mongo.Repository;

namespace Mvc8.Mongo.Services;

public class ProductService
{
    private readonly IMongoRepository<Product> _productRepository;

    public ProductService(IMongoRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Product> GetByIdAsync(string id)
    {
        return await _productRepository.GetByIdAsync(id);
    }
    
    public async Task<List<Product>> GetFirstThreeProductsByCategoryIdAsync(string categoryId)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        var options = new FindOptions<Product>
        {
            Limit = 3,
            Sort = Builders<Product>.Sort.Ascending(p => p.CreatedAt)
        };

        var result = await  _productRepository.Collection.FindAsync(filter, options);
        return await result.ToListAsync();
    }
}