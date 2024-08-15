using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Mvc8.Mongo.Models.MongoDb;
using Mvc8.Mongo.Repository;
using Mvc8.Mongo.Services;

namespace Mvc8.Mongo.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    public async Task<IActionResult> GetAllAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }
    
    public async Task<IActionResult> GetThirdLevelCategoriesAsync()
    {
        var categories = await _categoryService.GetThirdLevelCategoriesAsync();
        return Ok(categories);
    }
    
    public async Task<IActionResult> GetCategoryIdByNameAsync(string name)
    {
        var categoryId = await _categoryService.GetCategoryIdByNameAsync(name);
        return Ok(categoryId);
    }
    
}