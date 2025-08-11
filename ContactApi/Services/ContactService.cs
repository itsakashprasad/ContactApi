using ContactApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContactApi.Services;

public class ContactService
{
    private readonly IMongoCollection<ContactMessage> _collection;

    public ContactService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _collection = database.GetCollection<ContactMessage>(config["MongoDB:CollectionName"]);
    }

    public async Task<List<ContactMessage>> GetAsync()
    {
        Console.WriteLine("Fetching all contacts from MongoDB...");
        var list = await _collection.Find(_ => true).ToListAsync();
        Console.WriteLine($"MongoDB returned {list.Count} documents");
        return list;
    }
    public async Task CreateAsync(ContactMessage msg) =>
        await _collection.InsertOneAsync(msg);
}
