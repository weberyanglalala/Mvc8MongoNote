namespace Mvc8.Mongo.Models.MongoDb;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; }

    [BsonElement("customer")]
    public Customer Customer { get; set; }

    [BsonElement("items")]
    public List<OrderItem> Items { get; set; }

    [BsonElement("orderInfo")]
    public OrderInfo OrderInfo { get; set; }

    [BsonElement("shippingInfo")]
    public ShippingInfo ShippingInfo { get; set; }
}

public class Customer
{
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("address")]
    public Address Address { get; set; }
}

public class Address
{
    [BsonElement("street")]
    public string Street { get; set; }

    [BsonElement("city")]
    public string City { get; set; }

    [BsonElement("state")]
    public string State { get; set; }

    [BsonElement("zipCode")]
    public string ZipCode { get; set; }

    [BsonElement("country")]
    public string Country { get; set; }
}

public class OrderItem
{
    [BsonElement("product")]
    public ProductItem Product { get; set; }

    [BsonElement("quantity")]
    public int Quantity { get; set; }

    [BsonElement("subtotal")]
    public decimal Subtotal { get; set; }
}


public class ProductItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string Id { get; set; }

    [BsonElement("sku")]
    public string Sku { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("price")]
    public decimal Price { get; set; }
}

public class OrderInfo
{
    [BsonElement("orderNumber")]
    public string OrderNumber { get; set; }

    [BsonElement("orderDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime OrderDate { get; set; }

    [BsonElement("totalAmount")]
    public decimal TotalAmount { get; set; }

    [BsonElement("status")]
    public string Status { get; set; }

    [BsonElement("paymentMethod")]
    public string PaymentMethod { get; set; }

    [BsonElement("paymentStatus")]
    public string PaymentStatus { get; set; }
}

public class ShippingInfo
{
    [BsonElement("method")]
    public string Method { get; set; }

    [BsonElement("trackingNumber")]
    public string TrackingNumber { get; set; }

    [BsonElement("estimatedDeliveryDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime EstimatedDeliveryDate { get; set; }
}