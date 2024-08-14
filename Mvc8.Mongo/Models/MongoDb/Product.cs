namespace Mvc8.Mongo.Models.MongoDb;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("categoryId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("isActive")]
    public bool IsActive { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("sku")]
    public string Sku { get; set; }

    [BsonElement("stock")]
    public int Stock { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}