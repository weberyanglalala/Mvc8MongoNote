namespace Mvc8.Mongo.Models.MongoDb;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("ancestors")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string> Ancestors { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("image")]
    public string Image { get; set; }

    [BsonElement("isActive")]
    public bool IsActive { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("parent")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Parent { get; set; }

    [BsonElement("slug")]
    public string Slug { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}