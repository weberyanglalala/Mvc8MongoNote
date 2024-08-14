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
}