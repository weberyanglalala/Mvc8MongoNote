using MongoDB.Driver;

namespace Mvc8.Mongo.Repository;

public interface IMongoRepository<T> where T : class
{
    IMongoCollection<T> Collection { get; }
    Task<T> GetByIdAsync(string id);
    Task<List<T>> GetAllAsync();
    Task CreateAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
}