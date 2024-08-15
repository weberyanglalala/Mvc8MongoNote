using Microsoft.AspNetCore.Mvc;
using Mvc8.Mongo.Services;

namespace Mvc8.Mongo.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    public async Task<IActionResult> GetFirstThreeProductsByCategoryIdAsync([FromQuery]string categoryId)
    {
        var products = await _productService.GetFirstThreeProductsByCategoryIdAsync(categoryId);
        if (products == null)
        {
            return NotFound();
        }
        return Ok(products);
    }
}