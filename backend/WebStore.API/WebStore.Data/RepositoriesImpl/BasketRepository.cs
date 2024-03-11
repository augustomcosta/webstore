using StackExchange.Redis;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebStore.Data.RepositoriesImpl;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabaseAsync _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    
    public async Task<Basket> GetBasketAsync(Guid basketId)
    {
        var data = await _database.StringGetAsync(basketId.ToString());
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
    }

    public async Task<Basket> UpdateBasketAsync(Basket basket)
    {
        var created = 
            await _database.StringSetAsync(basket.Id.ToString(), JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
        if (!created) return null;
        return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> Delete(Guid basketId)
    {
        return await _database.KeyDeleteAsync(basketId.ToString());
    }
}