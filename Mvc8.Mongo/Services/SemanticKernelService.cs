using System.Diagnostics.CodeAnalysis;
using Mvc8.Mongo.Models.Settings;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Connectors.MongoDB;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using MongoDB.Driver;
using Mvc8.Mongo.Models.Dtos;
using Mvc8.Mongo.Models.MongoDb;

namespace Mvc8.Mongo.Services;

[Experimental("SKEXP0001")]
public class SemanticKernelService
{
    private readonly MemoryBuilder _memoryBuilder;
    private readonly ISemanticTextMemory _semanticTextMemory;
    private readonly MongoDbVectorSettings _mongoDbVectorSettings;
    private readonly MongoDBMemoryStore _mongoDBMemoryStore;
    private readonly string _openAiApiKey;
    private readonly string _connectionString;
    private readonly string _searchIndexName;
    private readonly string _databaseName;
    private readonly string _collectionName;
    private readonly MongoClient _mongoClient;

    public SemanticKernelService(MongoDbVectorSettings mongoDbVectorSettings, IConfiguration configuration)
    {
        _mongoDbVectorSettings = mongoDbVectorSettings;
        _connectionString = _mongoDbVectorSettings.ConnectionString;
        _searchIndexName = _mongoDbVectorSettings.SearchIndexName;
        _databaseName = _mongoDbVectorSettings.DatabaseName;
        _collectionName = _mongoDbVectorSettings.CollectionName;
        _openAiApiKey = configuration["OpenAIApiKey"];
        _mongoDBMemoryStore = new MongoDBMemoryStore(_connectionString, _databaseName, _searchIndexName);
        _memoryBuilder = new MemoryBuilder();
        _memoryBuilder.WithOpenAITextEmbeddingGeneration("text-embedding-ada-002", _openAiApiKey);
        _memoryBuilder.WithMemoryStore(_mongoDBMemoryStore);
        _semanticTextMemory = _memoryBuilder.Build();
        _mongoClient = new MongoClient(_connectionString);
    }
    
    public async Task<List<ProductSearchResult>> GetRecommendationsAsync(string userInput)
    {
        var memories = _semanticTextMemory.SearchAsync(_collectionName, userInput, limit: 10, minRelevanceScore: 0.6);
        
        var result = new List<ProductSearchResult>();
        await foreach(var memory in memories)
        {
            var productSearchResult = new ProductSearchResult
            {
                Id = memory.Metadata.Id,
                Description = memory.Metadata.Description,
                Name = memory.Metadata.AdditionalMetadata,
                Relevance = memory.Relevance.ToString("0.00")
            };
            result.Add(productSearchResult);
        }
        return result;
    }   
    
    public async Task FetchAndSaveProductDocumentsAsync(int limitSize)
    {
        await FetchAndSaveProductDocuments(_semanticTextMemory, limitSize);
    }
    
    private async Task FetchAndSaveProductDocuments(ISemanticTextMemory memory, int limitSize)
    {
        var productDatabase = _mongoClient.GetDatabase("demo");
        var productCollection = productDatabase.GetCollection<Product>("products");
        List<Product> productDocuments = new List<Product>();
        productDocuments = productCollection.Find(p => true).Limit(limitSize).ToList();
        foreach (var product in productDocuments)
        {
            try
            {
                Console.WriteLine($"Processing {product.Id}...");
                await memory.SaveInformationAsync(
                    collection: _collectionName,
                    text: product.Description,
                    id: product.Id,
                    description: product.Description,
                    additionalMetadata: product.Name
                    );
                Console.WriteLine($"Done {product.Id}...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}