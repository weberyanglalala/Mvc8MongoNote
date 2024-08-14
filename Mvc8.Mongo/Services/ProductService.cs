using Mvc8.Mongo.Models.MongoDb;
using Mvc8.Mongo.Repository;

namespace Mvc8.Mongo.Services;

public class ProductService
{
    private readonly IMongoRepository<Product> _mongoRepository;

    public ProductService(IMongoRepository<Product> mongoRepository)
    {
        _mongoRepository = mongoRepository;
    }
    
    public async Task<Product> GetByIdAsync(string id)
    {
        return await _mongoRepository.GetByIdAsync(id);
    }
}