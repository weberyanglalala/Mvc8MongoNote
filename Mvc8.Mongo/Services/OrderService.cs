using Mvc8.Mongo.Models.MongoDb;
using Mvc8.Mongo.Repository;

namespace Mvc8.Mongo.Services;

public class OrderService
{
    private readonly IMongoRepository<Order> _orderRepository;

    public OrderService(IMongoRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<Order> GetByIdAsync(string id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }
}