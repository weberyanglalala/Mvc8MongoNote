using Microsoft.AspNetCore.Mvc;
using Mvc8.Mongo.Services;

namespace Mvc8.Mongo.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }
}