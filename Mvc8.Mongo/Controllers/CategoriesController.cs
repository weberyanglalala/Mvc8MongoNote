using Microsoft.AspNetCore.Mvc;
using Mvc8.Mongo.Models.MongoDb;
using Mvc8.Mongo.Repository;

namespace Mvc8.Mongo.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController
{
    private readonly IMongoRepository<Category> _categoryRepository;

    public CategoriesController(IMongoRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Category> GetByIdAsync(string id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }
}