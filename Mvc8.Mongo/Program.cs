using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mvc8.Mongo.Models.MongoDb;
using Mvc8.Mongo.Models.Settings;
using Mvc8.Mongo.Repository;
using Mvc8.Mongo.Services;

namespace Mvc8.Mongo;

public class Program
{
    [Experimental("SKEXP0001")]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services
            .Configure<MongoDbVectorSettings>(builder.Configuration.GetSection(nameof(MongoDbVectorSettings)))
            .AddSingleton(settings => settings.GetRequiredService<IOptions<MongoDbVectorSettings>>().Value);

        builder.Services.AddSingleton<SemanticKernelService>();
        builder.Services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(builder.Configuration.GetConnectionString("MongoConnection")));
        
        builder.Services.AddScoped<IMongoRepository<Order>>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var dbName = "demo";
            var collectionName = "orders";
            return new MongoRepository<Order>(client, dbName, collectionName);
        });
        
        builder.Services.AddScoped<IMongoRepository<Product>>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var dbName = "demo";
            var collectionName = "products";
            return new MongoRepository<Product>(client, dbName, collectionName);
        });
        
        builder.Services.AddScoped<IMongoRepository<Category>>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var dbName = "demo";
            var collectionName = "categories";
            return new MongoRepository<Category>(client, dbName, collectionName);
        });

        builder.Services.AddScoped<OrderService>();
        builder.Services.AddScoped<CategoryService>();
        builder.Services.AddScoped<ProductService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}