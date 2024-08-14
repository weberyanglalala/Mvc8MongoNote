using Microsoft.AspNetCore.Mvc;
using Mvc8.Mongo.Models.MongoDb;
using Mvc8.Mongo.Repository;

namespace Mvc8.Mongo.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMongoRepository<Product> _productRepository;

    public ProductsController(IMongoRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}